namespace CodeBetter.Canvas.Web
{
    using System.Web.Security;

    public interface IAuthenticationManager
    {
        void SetAuthenticationToken(int developerId);
        void Signout();
    }

    public class AuthenticationManager : IAuthenticationManager
    {
        public void SetAuthenticationToken(int developerId)
        {
            FormsAuthentication.SetAuthCookie(developerId.ToString(), false);
        }

        public void Signout()
        {
            FormsAuthentication.SignOut();
        }
    }
}