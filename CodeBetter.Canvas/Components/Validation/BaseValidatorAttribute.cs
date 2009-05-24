
namespace CodeBetter.Canvas.Validation
{
    using System;
    using System.Collections.Generic;
    
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class BaseValidatorAttribute : Attribute
    {
        private object _entity;

        public abstract IDictionary<string, string> ToProperties();
        public abstract bool? IsValid(object value);

        public bool? IsValid(object entity, object value)
        {
            _entity = entity;
            return IsValid(value);
        }

    }
}
