namespace CodeBetter.Canvas.Validation
{
    using System.Collections.Generic;
    
    public class StringLengthAttribute : BaseValidatorAttribute
    {
        private readonly int _minimumLength;
        private readonly int _maximumLength;

        public int MinimumLength
        {
            get { return _minimumLength; }
        }
        public int MaximumLength
        {
            get { return _maximumLength; }
        }

        public StringLengthAttribute(int maximumLength) : this(0, maximumLength){}
        public StringLengthAttribute(int minimumLength, int maximumLength)
        {
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }

        public override IDictionary<string, string> ToProperties()
        {            
            return new Dictionary<string, string>
                       {
                           { "min", _minimumLength.ToString() },
                           { "max", _maximumLength.ToString() },
                       };
        }

        public override bool? IsValid(object value)
        {            
            if (!(value is string))
            {
                return false;
            }            
            var length = ((string)value).Length;
            return length >= _minimumLength && length <= _maximumLength;
        }
    }
}