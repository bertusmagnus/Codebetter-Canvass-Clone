namespace CodeBetter.Canvas.Validation
{
    using System.Collections.Generic;

    public class PatternAttribute : BaseValidatorAttribute
    {
        private readonly string _pattern;

        public PatternAttribute(string pattern)
        {
            _pattern = pattern;
        }

        public override IDictionary<string, string> ToProperties()
        {
            return new Dictionary<string, string> { { "pattern", string.Concat("/", _pattern, "/") } };
        }

        public override bool? IsValid(object value)
        {
            if (!(value is string))
            {
                return false;
            }
            return System.Text.RegularExpressions.Regex.IsMatch((string)value, _pattern);            
        }
    }
}