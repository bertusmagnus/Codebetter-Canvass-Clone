namespace CodeBetter.Canvas.Tests.Web.Controllers.ManageControllerTests
{
    using Canvas.Web;
    using Rhino.Mocks;
    using Xunit;

    public class ViewTests : BaseControllerTests
    {
        [Fact]
        public void RendersViewWithUserModel()
        {
            var repository = Dynamic<IUserRepository>();
            var user = new User();                        
            var controller = new ManageController(repository);

            repository.Expect(r => r.Find<User>(85)).Return(user);
            ReplayAll();

            MvcAssert.DefaultViewWithModel(user, controller.View(85));
            repository.VerifyAllExpectations();
        }
    }
}