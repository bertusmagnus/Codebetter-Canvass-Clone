namespace CodeBetter.Canvas.Tests.Web.HomeControllerTests
{
    using Rhino.Mocks;
    using Canvas.Web;
    using Xunit;

    public class RegisterTests : BaseControllerTests
    {
        [Fact]
        public void ReturnsDefaultViewWithNoModel()
        {
            MvcAssert.DefaultView(new HomeController(null).Register());            
        }

        [Fact]
        public void ReturnsWithValidationErrors()
        {
            var user = new User();
            var controller = new HomeController(null);

            controller.ModelState.AddModelError(string.Empty, string.Empty);
            var result = controller.Register(user);
            MvcAssert.DefaultViewWithModel(user, result);
        }

        [Fact]
        public void SavesTheUser()
        {
            var user = new User { Credentials = new Credentials() };
            var repository = Dynamic<IUserRepository>();
            var controller = new HomeController(repository);

            repository.Expect(r => r.Save(user));

            ReplayAll();
            controller.Register(user);
            repository.VerifyAllExpectations();
        }
        [Fact]
        public void DisplaysSuccessView()
        {
            var user = new User { Credentials = new Credentials { Email = "giru@playa.cool" } };
            var repository = Dynamic<IUserRepository>();
            var controller = new HomeController(repository);

            repository.Stub(r => r.Save(user));

            ReplayAll();
            MvcAssert.ViewName("RegistrationSuccessful", controller.Register(user));
        }
    }
}