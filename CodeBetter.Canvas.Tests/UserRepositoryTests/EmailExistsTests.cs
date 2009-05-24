namespace CodeBetter.Canvas.Tests.MappingTests.UserRepositoryTests
{
    using Xunit;

    public class EmailExistsTests : DatabaseFixture
    {
        [Fact]
        public void ReturnsFalseIfEmailIsntInUse()
        {
            var user = new User {Credentials = new Credentials {Email = "a@a.com"}};
            var repository = new UserRepository(SessionSource.CreateSession());
            Assert.False(repository.EmailExists(user));
        }

        [Fact]
        public void ReturnsFalseIfEmailBelongsToUser()
        {
            var repository = new UserRepository(SessionSource.CreateSession());
            var user = new User { Name = "test", Credentials = new Credentials("a@a.com", "pass") };

            repository.Save(user);
            Assert.False(repository.EmailExists(user));
        }

        [Fact]
        public void ReturnsTrueIfEmailExistsForDifferentUser()
        {
            var repository = new UserRepository(SessionSource.CreateSession());
            var user = new User { Name = "test", Credentials = new Credentials("a@a.com", "pass") };
            repository.Save(new User {Name = "test", Credentials = new Credentials("a@a.com", "pass")});
            Assert.True(repository.EmailExists(user));
        }
    }
}