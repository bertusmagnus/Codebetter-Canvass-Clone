namespace CodeBetter.Canvas.Tests.Web.HomeControllerTests
{
    using Canvas.Web;
    using Rhino.Mocks;
    using Xunit;

    public class LogoutTests : BaseControllerTests
    {
        [Fact]
        public void SignsOutUser()
        {
            var authenticator = Dynamic<IAuthenticationManager>();

            authenticator.Expect(a => a.Signout());
            ReplayAll();

            new HomeController(authenticator, null).Logout();
            authenticator.VerifyAllExpectations();
        }
        [Fact]
        public void DisplaysHomePage()
        {
            MvcAssert.ViewName("index", new HomeController( Dynamic<IAuthenticationManager>(), null).Logout());
        }
    }
}