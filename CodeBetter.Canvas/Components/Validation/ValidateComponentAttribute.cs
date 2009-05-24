namespace CodeBetter.Canvas.Validation
{
    using System.Collections.Generic;

    public class ValidateComponentAttribute : BaseValidatorAttribute
    {
        public bool Required { get; private set; }

        public ValidateComponentAttribute() : this(true){}
        public ValidateComponentAttribute(bool required)
        {
            Required = required;
        }

        public override IDictionary<string, string> ToProperties()
        {
            throw new System.NotImplementedException();
        }

        public override bool? IsValid(object value)
        {
            if (value == null)
            {
                return !Required;
            }
            return null;
        }
    }
}