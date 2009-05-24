namespace CodeBetter.Canvas.Validation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Helpers;
    using System.Linq;
    
    public class ValidatorConfiguration
    {
        private static IDictionary<Type, EntityValidationInfo> _rules;

        public static IDictionary<Type, EntityValidationInfo> Rules
        {
            get { return _rules; }
        }

        public static void Initialize(params string[] assemblyNames)
        {
            _rules = new Dictionary<Type, EntityValidationInfo>(10);
            foreach (var assemblyName in assemblyNames)
            {
                foreach (var kvp in LoadFromAssembly(Assembly.Load(assemblyName)))
                {
                    _rules.Add(kvp.Key, kvp.Value);
                }                
            }
            foreach(var entity in _rules.Values)
            {                                
                foreach(var property in entity.Properties)
                {
                    if (property.Validators[0] is ValidateComponentAttribute)
                    {
                        property.Component = _rules[property.Property.PropertyType];
                    }                    
                }
            }
        }
        private static IDictionary<Type, EntityValidationInfo> LoadFromAssembly(Assembly assembly)
        {
            var rules = new Dictionary<Type, EntityValidationInfo>(10);
            foreach (var type in assembly.GetExportedTypes())
            {
                var info = GetEntityMeta(type);
                if (info != null && info.Count > 0) { rules.Add(type, new EntityValidationInfo(info)); }
            }
            return rules;
        }
        private static IList<PropertyValidationInfo> GetEntityMeta(Type type)
        {
            var properties = new List<PropertyValidationInfo>();
            foreach (var property in type.GetProperties())
            {
                var attributes = (BaseValidatorAttribute[])property.GetCustomAttributes(typeof(BaseValidatorAttribute), true);
                if (attributes.Length == 0) { continue; }                
                properties.Add(new PropertyValidationInfo(property, attributes));
            }
            return properties;            
        }
    }

    public class EntityValidationInfo
    {
        public IList<PropertyValidationInfo> Properties { get; private set; }
        public EntityValidationInfo(IEnumerable<PropertyValidationInfo> properties)
        {
            var sorted = from p in properties orderby p.Property.Name select p;
            Properties = new List<PropertyValidationInfo>(sorted.AsEnumerable());
        }
    }
    public class PropertyValidationInfo
    {
        public EntityValidationInfo Component { get; set; }
        public IList<BaseValidatorAttribute> Validators { get; private set; }
        public PropertyInfo Property { get; private set; }
        public PropertyValidationInfo(PropertyInfo property, IEnumerable<BaseValidatorAttribute> validators)
        {
            Property = property;
            var sorted = from v in validators orderby v.GetType().Name select v;
            Validators = new List<BaseValidatorAttribute>(sorted.AsEnumerable());            
        }
    }
}