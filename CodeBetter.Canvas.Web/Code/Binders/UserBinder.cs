namespace CodeBetter.Canvas.Web.Binders
{
    using Canvas.Components;
    using Validation;

    public class UserBinder : ModelBinder<User>
    {
        private readonly IUserRepository _repository;
        private readonly IEncryptor _encryptor;

        public UserBinder(IEncryptor encryptor, IValidator validator) : this(encryptor, null, validator){}
        public UserBinder(IEncryptor encryptor, IUserRepository repository, IValidator validator): base(repository, validator)
        {
            _encryptor = encryptor;
            _repository = repository;
        }        
        public override User BindNew()
        {
            var user = BindBase(new User());
            user.Credentials.Password = _encryptor.Encrypt(Get("Credentials.Password"));
            return user;
        }

        public override User BindExisting(User original)
        {
            throw new System.NotImplementedException();
        }

        private User BindBase(User user)
        {
            user.Name = Get("Name");
            user.Credentials.Email = Get("Credentials.Email");
            return user;
        }

        protected override ValidationError[] Validate(User entity)
        {
            if (_repository.EmailExists(entity))
            {
                return new[] {new ValidationError("Credentials_Email", "The email is already in use")};
            }
            return null;
        }
    }
}