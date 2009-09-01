namespace CodeBetter.Canvas.Tests.Web.ValidatorTests
{
    using Canvas.Web;
    using Tests.ValidatorTests;
    using Validation;
    using Xunit;

    public class BuildJson : ValidationFixture
    {
        [Fact]        
        public void BuildsJsonString()
        {
            HtmlValidationExtensions.Initialize(ValidatorConfiguration.Rules);
            var rule = HtmlValidationExtensions.RulesFor<MockValidationEntity>(null);
            Assert.Equal("rules:{Email:{pattern:/.+@.+\\..+/,required:true,tip:'Make it valid!'},Name:{required:true},Security_Password:{required:true,min:0,max:20}}", rule);
        }
    }
}