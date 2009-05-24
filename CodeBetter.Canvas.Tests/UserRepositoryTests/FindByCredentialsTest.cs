namespace CodeBetter.Canvas.Tests.MappingTests.UserRepositoryTests
{    
    using Xunit;
    
    public class FindByCredentialsTest : DatabaseFixture
    {
        [Fact]
        public void ReturnsNullIfUserNotFound()
        {
            var repository = new UserRepository(SessionSource.CreateSession());
            Assert.Null(repository.FindByCredentials(new Credentials("18@android.cool", "marron")));
        }

        [Fact]
        public void ReturnsNullIfPasswordsDontMatch()
        {
            var repository = new UserRepository(SessionSource.CreateSession());

            ReplayAll();
            repository.Save(new User { Name = "#18", Credentials = new Credentials("18@android.cool", "secret")});
            Assert.Null(repository.FindByCredentials(new Credentials("18@android.cool", "marron")));            
        }

        [Fact]
        public void ReturnsUserIfCredentialsAreValid()
        {
            var repository = new UserRepository(SessionSource.CreateSession());
            

            ReplayAll();
            repository.Save(new User { Name = "#18", Credentials = new Credentials("18@android.cool", "secret") });
            var user = repository.FindByCredentials(new Credentials("18@android.cool", "secret"));
            Assert.Equal("#18", user.Name);
        }
    }
}