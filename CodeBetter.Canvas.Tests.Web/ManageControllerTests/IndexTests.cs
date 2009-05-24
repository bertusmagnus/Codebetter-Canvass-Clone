namespace CodeBetter.Canvas.Tests.Web.Controllers.ManageControllerTests
{
    using Canvas.Web;
    using Rhino.Mocks;
    using Xunit;

    public class IndexTests : BaseControllerTests
    {
        [Fact]
        public void ReturnsDefaultViewWithSummary()
        {
            var repository = Dynamic<IUserRepository>();
            var expected = new PagedList<User>();
            var controller = new ManageController(repository);            

            repository.Stub(r => r.GetUserList(Arg<Pager>.Is.Anything)).Return(expected);
            ReplayAll();

            MvcAssert.DefaultViewWithModel(expected, controller.Index(0));
        }

        [Fact]
        public void LoadsPage()
        {
            var repository = Dynamic<IUserRepository>();
            var controller = new ManageController(repository);            
            
            repository.Stub(r => r.GetUserList(Arg<Pager>.Matches(p => p.CurrentPage == 5))).Return(null);
            ReplayAll();

            controller.Index(5);
        }

        [Fact]
        public void ForcesLimitOf20Records()
        {
            var repository = Dynamic<IUserRepository>();
            var controller = new ManageController(repository);

            repository.Stub(r => r.GetUserList(Arg<Pager>.Matches(p => p.RecordsPerPage == 20))).Return(null);
            ReplayAll();

            controller.Index(1);
        }
    }
}