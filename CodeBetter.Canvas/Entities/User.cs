namespace CodeBetter.Canvas
{
    using Validation;

    public class User : Entity<int>, IEntity
    {
        private string _name;
        private Credentials _credentials;

        [Required, StringLength(2, 30), Tip("Please enter your name. 2-30 characters")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [ValidateComponent]
        public Credentials Credentials
        {
            get { return _credentials; }
            set { _credentials = value; }
        }

        public User()
        {
            Credentials = new Credentials();
        }
    }
}