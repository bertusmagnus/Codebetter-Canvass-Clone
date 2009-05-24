namespace CodeBetter.Canvas.Tests.MappingTests.UserRepositoryTests
{
    using Components;
    using Rhino.Mocks;
    using Xunit;
    
    public class FindByCredentialsTest : DatabaseFixture
    {
        [Fact]
        public void ReturnsNullIfUserNotFound()
        {
            var repository = new UserRepository(null, SessionSource.CreateSession());
            Assert.Null(repository.FindByCredentials(new Credentials("18@android.cool", "marron")));
        }

        [Fact]
        public void ReturnsNullIfPasswordsDontMatch()
        {
            var encryptor = Dynamic<IEncryptor>();
            var repository = new UserRepository(encryptor, SessionSource.CreateSession());

            encryptor.Expect(e => e.IsMatch("marron", "hashed")).Return(false);

            ReplayAll();
            repository.Save(new User { Name = "#18", Credentials = new Credentials { Email = "18@android.cool", Password = "hashed" } });
            Assert.Null(repository.FindByCredentials(new Credentials("18@android.cool", "marron")));
            encryptor.VerifyAllExpectations();
        }

        [Fact]
        public void ReturnsDeveloperIfCredentialsAreValid()
        {
            var encryptor = Dynamic<IEncryptor>();
            var repository = new UserRepository(encryptor, SessionSource.CreateSession());

            encryptor.Stub(e => e.IsMatch(null, null)).IgnoreArguments().Return(true);

            ReplayAll();
            repository.Save(new User { Name = "#18", Credentials = new Credentials { Email = "18@android.cool", Password = "hashed" } });
            var user = repository.FindByCredentials(new Credentials("18@android.cool", "marron"));
            Assert.Equal("#18", user.Name);
        }
    }
}