namespace CodeBetter.Canvas.Components
{
    using FluentNHibernate.Cfg;
    using NHibernate;

    public interface ISessionSource
    {
        ISession CreateSession();
        void Close();
        void BuildSchema();
    }

    public class SessionSource : FluentNHibernate.SessionSource, ISessionSource
    {
        private ISession _currentSession;
        public SessionSource(FluentConfiguration config) : base(config) { }

        public override ISession CreateSession()
        {
            if (_currentSession == null || !_currentSession.IsOpen)
            {
                _currentSession = base.CreateSession();
            }
            return _currentSession;
        }

        public void Close()
        {
            if (_currentSession != null)
            {
                _currentSession.Dispose();
            }
        }

    }
}
