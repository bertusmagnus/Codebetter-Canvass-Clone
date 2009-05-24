namespace CodeBetter.Canvas.Tests.ValidatorTests
{
    using Validation;
    using Xunit;

    public class StringLengthTests
    {
        [Fact]
        public void IsInvalidIfValueIsntAString()
        {
            Assert.False(new StringLengthAttribute(1).IsValid(new object()).Value);
        }        
        [Fact]
        public void DefaultMinimumIsZero()
        {
            Assert.Equal(0, new StringLengthAttribute(5).MinimumLength);
        }
        [Fact]
        public void IsInvalidWhenStringLengthOutsideOfRange()
        {
            Assert.False(new StringLengthAttribute(2, 4).IsValid("1").Value);
            Assert.False(new StringLengthAttribute(2, 4).IsValid("12345").Value);            
        }  
        [Fact]
        public void IsValidWhenStringLengthWithinRange()
        {
            Assert.True(new StringLengthAttribute(2, 4).IsValid("12").Value);
            Assert.True(new StringLengthAttribute(2, 4).IsValid("123").Value);
            Assert.True(new StringLengthAttribute(2, 4).IsValid("1234").Value);
        }        
    }
}