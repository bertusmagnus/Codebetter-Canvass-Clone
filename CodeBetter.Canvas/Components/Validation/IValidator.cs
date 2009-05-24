namespace CodeBetter.Canvas.Validation
{
    using System.Collections.Generic;

    public interface IValidator
    {
        bool Validate(object o, out ValidationError[] errors);
    }

    public class Validator : IValidator
    {
        public bool Validate(object o, out ValidationError[] e)
        {
            var errors = new List<ValidationError>(10);
            var propertyErrors = ValidateProperties(o, string.Empty);
            if (propertyErrors != null)
            {
                errors.AddRange(propertyErrors);
            }
            var customErrors = ValidateCustom(o);
            if (customErrors != null)
            {
                errors.AddRange(customErrors);
            }           
            e = errors.ToArray();
            return e.Length == 0;            
        }        

        private static ICollection<ValidationError> ValidateProperties(object o, string chain)
        {                        
            if (!ValidatorConfiguration.Rules.ContainsKey(o.GetType()))
            {
                return null;
            }
            var errors = new List<ValidationError>(10);
            foreach (var property in ValidatorConfiguration.Rules[o.GetType()].Properties)
            {    
                var value = property.Property.GetValue(o, null);
                foreach (var validator in property.Validators)
                {
                    var isValid = validator.IsValid(o, value);
                    if (isValid == null)
                    {
                        var componentErrors = ValidateProperties(value, chain + property.Property.Name + "_");
                        if (componentErrors != null)
                        {
                            errors.AddRange(componentErrors);
                        }
                        continue;                        
                    }                    
                    if (isValid.Value) { continue; }
                    errors.Add(new ValidationError(chain + property.Property.Name));
                    break;
                }
            }
            return errors;
        }
        private ICollection<ValidationError> ValidateCustom(object o)
        {
            if (!(o is IValidate))
            {
                return null;
            }
            return ((IValidate) o).Validate();            
        }
    }
}