namespace CodeBetter.Canvas
{
    using System.Linq;
    using Components;
    using NHibernate;

    public interface IUserRepository : IRepository
    {
        User FindByCredentials(Credentials credentials);
        bool EmailExists(User user);
    }

    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(ISessionSource source) : base(source){}
        public UserRepository(ISession session) : base(session){}


        public User FindByCredentials(Credentials credentials)
        {            
            var user = (from d in Context().Users
                             where d.Credentials.Email == credentials.Email
                             select d).SingleOrDefault();

            if (user != null && credentials.Password == user.Credentials.Password)
            {
                return user;
            }
            return null;
        }

        public bool EmailExists(User user)
        {
            return (from d in Context().Users
                    where d.Credentials.Email == user.Credentials.Email
                          && d.Id != user.Id
                    select d).Any();
                                    
        }
    }
}