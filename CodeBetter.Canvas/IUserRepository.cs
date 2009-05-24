namespace CodeBetter.Canvas
{
    using System.Linq;
    using Components;
    using NHibernate;

    public interface IUserRepository : IRepository
    {
        User FindByCredentials(Credentials credentials);
        bool EmailExists(User user);
        PagedList<User> GetUserList(Pager pager);
    }

    public class UserRepository : Repository, IUserRepository
    {
        private readonly IEncryptor _encryptor;
        public UserRepository(IEncryptor encryptor, ISessionSource source) : base(source)
        {
            _encryptor = encryptor;
        }
        public UserRepository(IEncryptor encryptor, ISession session) : base(session)
        {
            _encryptor = encryptor;
        }


        public User FindByCredentials(Credentials credentials)
        {            
            var user = (from d in Context().Users
                             where d.Credentials.Email == credentials.Email
                             select d).SingleOrDefault();

            if (user != null && _encryptor.IsMatch(credentials.Password, user.Credentials.Password))
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

        public PagedList<User> GetUserList(Pager pager)
        {            
            var baseQuery = from user in Context().Users
                            orderby user.Name
                            select user;

            var dataQuery = baseQuery.Skip(pager.FirstRecord).Take(pager.RecordsPerPage);
            return new PagedList<User>(pager, dataQuery.ToArray(), baseQuery.Count());
        }
    }
}