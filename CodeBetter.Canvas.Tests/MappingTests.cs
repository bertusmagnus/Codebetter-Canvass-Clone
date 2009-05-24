namespace CodeBetter.Canvas.Tests.MappingTests
{
    using FluentNHibernate.Testing;
    using Xunit;
     
    
    public class MappingTests : DatabaseFixture
    {
        [Fact(Skip = "How to test componenet?")]
        public void UserSave()
        {
            new PersistenceSpecification<User>(SessionSource)
                .CheckProperty(x => x.Name, "Buu")
                .CheckProperty(x => x.Credentials.Password, "Bee")
                .CheckProperty(x => x.Credentials.Email, "buu@majin.cool")                    
                .CheckProperty(x => x.Id, 1)
                .VerifyTheMappings();
        }
    }
}