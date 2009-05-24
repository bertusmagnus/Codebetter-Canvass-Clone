namespace CodeBetter.Canvas.Tests.ValidatorTests
{
    using Validation;

    public class MockValidationEntity : IValidate
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, Pattern(".+@.+\\..+"), Tip("Make it valid!")]
        public string Email { get; set; }
        [ValidateComponent]
        public MockValidationComponent Security { get; set; }

        public ValidationError[] Validate()
        {
            if (Id == 9001)
            {
                return new[] { new ValidationError("Id", "It's over what?!"), };
            }
            return null;
        }
    }

    public class MockValidationComponent
    {
        [Required, StringLength(20)]
        public string Password { get; set; }
    }
}