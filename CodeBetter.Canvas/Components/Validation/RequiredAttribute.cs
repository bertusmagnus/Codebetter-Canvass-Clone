namespace CodeBetter.Canvas.Validation
{
    using System.Collections.Generic;
    
    public class RequiredAttribute : BaseValidatorAttribute
    {
        public override IDictionary<string, string> ToProperties()
        {
            return new Dictionary<string, string> {{"required", "true"}};
        }

        public override bool? IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            return !(value is string) || ((string)value).Length != 0;
        }
    }
}