namespace CodeBetter.Canvas.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;        
    using Rhino.Mocks;        

    public class BaseFixture
    {
        public MockRepository Mocks { get; private set; }
        
        public BaseFixture()
        {
            Mocks = new MockRepository(); 
        }

        public void ReplayAll()
        {
            Mocks.ReplayAll();
        }
        public T Dynamic<T>(params object[] arguments) where T : class
        {
            return Mocks.DynamicMock<T>(arguments);  //todo inject these
        }
        public T Partial<T>(params object[] arguments) where T : class
        {
            return Mocks.PartialMock<T>(arguments);
        }        
        

        public static T CreateWithId<T>(int value) where T : new()
        {
            return CreateWithFields<T>(new {_id = value});
        }
        public static T CreateWithFields<T>(object dictionary) where T : new()
        {
            var o = new T();
            var fieldHash = ToPropertyHash(dictionary);
            foreach(var keyValue in fieldHash)
            {                
                SetField(o, keyValue.Key, keyValue.Value);    
            }            
            return o;
        }

        private static void SetField(object o, string fieldName, object value)
        {
            FieldInfo field = null;
            for (var type = o.GetType(); field == null && type.BaseType != null; type = type.BaseType)
            {
                field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            }
            if (field == null)
            {
                throw new ArgumentException("Couldn't not find field with that name");
            }
            field.SetValue(o, value);
        }
        private static IDictionary<string, object> ToPropertyHash(object @object)
        {
            var properties = TypeDescriptor.GetProperties(@object);
            var hash = new Dictionary<string, object>(properties.Count);
            foreach (PropertyDescriptor descriptor in properties)
            {
                hash.Add(descriptor.Name, descriptor.GetValue(@object));
            }
            return hash;       
        }
    }  
}