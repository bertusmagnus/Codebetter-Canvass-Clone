namespace CodeBetter.Canvas.Tests.ValidatorTests
{
    using Validation;
    using Xunit;

    public class ConfigurationTests : ValidationFixture
    {
        [Fact]
        public void LoadsEntityInformation()
        {
            var rules = ValidatorConfiguration.Rules[typeof(MockValidationEntity)];
            Assert.Equal(3, rules.Properties.Count);

            Assert.Equal("Email", rules.Properties[0].Property.Name);
            Assert.Equal(2, rules.Properties[0].Validators.Count);
            Assert.True(rules.Properties[0].Validators[0].GetType() != rules.Properties[0].Validators[1].GetType());
            Assert.True(typeof(RequiredAttribute).IsAssignableFrom(rules.Properties[0].Validators[0].GetType()) || typeof(PatternAttribute).IsAssignableFrom(rules.Properties[0].Validators[0].GetType()));
            Assert.True(typeof(RequiredAttribute).IsAssignableFrom(rules.Properties[0].Validators[1].GetType()) || typeof(PatternAttribute).IsAssignableFrom(rules.Properties[0].Validators[1].GetType()));

            Assert.Equal("Name", rules.Properties[1].Property.Name);
            Assert.Equal(1, rules.Properties[1].Validators.Count);
            Assert.IsAssignableFrom(typeof(RequiredAttribute), rules.Properties[1].Validators[0]);          
        }
        [Fact]
        public void LoadsComponent()
        {
            var rules = ValidatorConfiguration.Rules[typeof(MockValidationEntity)];
            Assert.NotNull(rules.Properties[2].Component);
        }
    }
   
}