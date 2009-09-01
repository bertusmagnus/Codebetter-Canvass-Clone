namespace CodeBetter.Canvas
{
    using System;
    using Components;
    using NHibernate;

    public interface IRepository
    {
        T Find<T>(int id);        
        void Delete<T>(T target);
        void Save<T>(T target);
        ModelContext Context();
    }


    public abstract class Repository : IRepository
    {
        private readonly ISession _session;

        protected Repository(ISessionSource source) : this(source.CreateSession()) { }
        protected Repository(ISession session)
        {
            _session = session;
            _session.FlushMode = FlushMode.Commit;
        }

        public T Find<T>(int id)
        {
            return _session.Get<T>(id);
        }
  
        public void Delete<T>(T target)
        {
            WithinTransaction(s => s.Delete(target));
        }
        public void Save<T>(T target)
        {
            WithinTransaction(s => s.SaveOrUpdate(target));
        }
        public void Save(params IEntity[] entities)
        {
            WithinTransaction(s => entities.Each(s.SaveOrUpdate));
        }
        public ModelContext Context()
        {
            return new ModelContext(_session);
        }

        protected void WithinTransaction(Action<ISession> action)
        {
            var transaction = _session.BeginTransaction();
            try
            {
                action(_session);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}