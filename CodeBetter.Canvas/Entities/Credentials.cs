namespace CodeBetter.Canvas
{
    using Validation;

    public class Credentials : IComponent
    {
        private string _password;
        private string _email;

        [Required, StringLength(4, 100), Tip("Please enter your password")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        [Required, StringLength(50), Pattern(".+@.+\\..+"), Tip("Please enter a valid email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public Credentials(){}
        public Credentials(string email, string password)
        {            
            _email = email;
            _password = password;
        }
    }
}