namespace CodeBetter.Canvas.Components
{
    public interface IEncryptor
    {
        string Encrypt(string plainText);
        bool IsMatch(string plainText, string hashed);       
    }

    public class Encryptor : IEncryptor
    {
        public string Encrypt(string plainText)
        {
            return BCrypt.HashPassword(plainText, BCrypt.GenerateSalt());
        }
        public bool IsMatch(string plainText, string hashed)
        {
            return BCrypt.CheckPassword(plainText, hashed);
        }
    }
}