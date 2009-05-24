namespace CodeBetter.Canvas.Tests.ValidatorTests
{
    using Validation;
    using Xunit;

    public class ValidateTests : ValidationFixture
    {
        [Fact]
        public void ReturnsTrueWithNoErrorsIfNoValidationNeeded()
        {
            ValidationError[] errors;
            Assert.True(new Validator().Validate(new object(), out errors));
            Assert.Equal(0, errors.Length);
        }

        [Fact]
        public void ReturnsTrueWithNoErrorsIfValidationPassed()
        {
            var entity = new MockValidationEntity { Name = "Picolo", Email = "pic@sevenballs.org", Security = new MockValidationComponent { Password = "i am valid" } };
            ValidationError[] errors;
            Assert.True(new Validator().Validate(entity, out errors));
            Assert.Equal(0, errors.Length);
        }

        [Fact]
        public void ReturnsFalseWithErrorsIfAttributeValidationFailed()
        {
            var entity = new MockValidationEntity { Email = "pic@sevenballs.org", Security = new MockValidationComponent { Password = "i am valid" } };
            ValidationError[] errors;
            Assert.False(new Validator().Validate(entity, out errors));
            Assert.Equal(1, errors.Length);
            Assert.Equal("Name", errors[0].Field);
            Assert.Equal(null, errors[0].Message);
        }

        [Fact]
        public void ReturnsFalseWithErrorsIfCustomValidationFailed()
        {
            var entity = new MockValidationEntity { Id = 9001, Name = "Picolo", Email = "pic@sevenballs.org", Security = new MockValidationComponent { Password = "i am valid" } };
            ValidationError[] errors;
            Assert.False(new Validator().Validate(entity, out errors));
            Assert.Equal(1, errors.Length);
            Assert.Equal("Id", errors[0].Field);
            Assert.Equal("It's over what?!", errors[0].Message);
        }

        [Fact]
        public void MergesAttributeAndCustomErrors()
        {
            var entity = new MockValidationEntity { Id = 9001, Name = "Picolo", Security = new MockValidationComponent { Password = "i am valid" } };
            ValidationError[] errors;
            Assert.False(new Validator().Validate(entity, out errors));
            Assert.Equal(2, errors.Length);
            Assert.Equal("Email", errors[0].Field);
            Assert.Equal("Id", errors[1].Field);
        }

        [Fact]
        public void ValidatesRequiredNullComponent()
        {
            var entity = new MockValidationEntity { Name = "Picolo", Email = "pic@sevenballs.org", Security = null };
            ValidationError[] errors;
            Assert.False(new Validator().Validate(entity, out errors));
            Assert.Equal(1, errors.Length);
            Assert.Equal("Security", errors[0].Field);
        }
        [Fact]
        public void ValidatesComponentProperties()
        {
            var entity = new MockValidationEntity { Name = "Picolo", Email = "pic@sevenballs.org", Security = new MockValidationComponent { Password = string.Empty } };
            ValidationError[] errors;
            Assert.False(new Validator().Validate(entity, out errors));
            Assert.Equal(1, errors.Length);
            Assert.Equal("Security_Password", errors[0].Field);
        }
    }
}