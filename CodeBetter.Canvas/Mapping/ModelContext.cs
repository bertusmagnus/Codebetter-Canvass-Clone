namespace CodeBetter.Canvas
{
    using System.Linq;
    using FluentNHibernate;
    using NHibernate;
    using NHibernate.Linq;

    public class ModelContext : NHibernateContext
    {
        public ModelContext(ISessionSource source) : this(source.CreateSession()) { }
        public ModelContext(ISession session) : base(session) { }

        public IOrderedQueryable<User> Users
        {
            get { return Session.Linq<User>(); }
        }
    }
}