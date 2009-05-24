namespace CodeBetter.Canvas.Tests.ValidatorTests
{
    using Validation;
    using Xunit;

    public class RequiredTests
    {
        [Fact]
        public void IsInvalidForNull()
        {
            Assert.False(new RequiredAttribute().IsValid(null).Value);
        }
        [Fact]
        public void IsInvalidForEmptyString()
        {
            Assert.False(new RequiredAttribute().IsValid(string.Empty).Value);
        }
        [Fact]
        public void IsValidForObject()
        {
            Assert.True(new RequiredAttribute().IsValid(new object()).Value);
        }
        [Fact]
        public void IsValidNonEmptyString()
        {
            Assert.True(new RequiredAttribute().IsValid("a").Value);
        }
    }
}