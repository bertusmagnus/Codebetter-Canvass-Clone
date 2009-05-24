namespace CodeBetter.Canvas.Tests.Web.HomeControllerTests
{
    using Canvas.Web;
    using Rhino.Mocks;
    using Xunit;

    public class LoginTests : BaseControllerTests
    {
        [Fact]
        public void DisplaysLoginScreen()
        {
            MvcAssert.DefaultView(new HomeController(null, null).Login());
        }
        [Fact]
        public void DisplaysLoginScreenWhenCredentialsArentValid()
        {
            var credentials = new Credentials();
            var controller = new HomeController(null, null);

            controller.ModelState.AddModelError(string.Empty, string.Empty);
            MvcAssert.DefaultViewWithModel(credentials, controller.Login(credentials));
        }
        [Fact]
        public void DisplaysLoginScreenUsersCantBeFound()
        {
            var repository = Dynamic<IUserRepository>();
            var credentials = new Credentials();
            var controller = new HomeController(null, repository);

            repository.Stub(r => r.FindByCredentials(Arg<Credentials>.Is.Anything)).Return(null);
            ReplayAll();

            MvcAssert.DefaultViewWithModel(credentials, controller.Login(credentials));
        }
        [Fact]
        public void GetsUserFromRepository()
        {
            var repository = Dynamic<IUserRepository>();
            var credentials = new Credentials { Email = "a@a.com", Password = "pass" };
            var controller = new HomeController(null, repository);

            repository.Expect(r => r.FindByCredentials(credentials)).Return(null);
            ReplayAll();

            controller.Login(credentials);
            repository.VerifyAllExpectations();
        }
        [Fact]
        public void SetsAuthenticationTokenForUser()
        {
            var repository = Dynamic<IUserRepository>();
            var authenticator = Dynamic<IAuthenticationManager>();
            var credentials = new Credentials { Email = "a@a.com", Password = "pass" };
            var controller = new HomeController(authenticator, repository);

            repository.Stub(r => r.FindByCredentials(credentials)).Return(new User { Id = 5 });
            authenticator.Expect(e => e.SetAuthenticationToken(5));
            ReplayAll();

            controller.Login(credentials);
            authenticator.VerifyAllExpectations();
        }
        [Fact]
        public void RedirectsAuthenticatedUserToManager()
        {
            var repository = Dynamic<IUserRepository>();
            var authenticator = Dynamic<IAuthenticationManager>();
            var credentials = new Credentials { Email = "a@a.com", Password = "pass" };
            var controller = new HomeController(authenticator, repository);

            repository.Stub(r => r.FindByCredentials(credentials)).Return(new User());
            authenticator.Stub(e => e.SetAuthenticationToken(Arg<int>.Is.Anything));
            ReplayAll();

            MvcAssert.RedirectTo("/Manage/Index", controller.Login(credentials));
        }
    }
}