namespace CodeBetter.Canvas.Tests.ValidatorTests
{
    using Validation;
    using Xunit;

    public class PatternTests
    {
        [Fact]
        public void IsInvalidIfValueIsntAString()
        {
            Assert.False(new PatternAttribute(string.Empty).IsValid(new object()).Value);
        }
        [Fact]
        public void IsInvalidIfValueDoesntMatchPattern()
        {
            Assert.False(new PatternAttribute("\\d{2}").IsValid("a").Value);
        }
        [Fact]
        public void IsValidIfValueMatchesPattern()
        {
            Assert.True(new PatternAttribute("\\d{2}").IsValid("12").Value);
        }
    }
}