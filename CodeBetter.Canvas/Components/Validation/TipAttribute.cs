namespace CodeBetter.Canvas.Validation
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class TipAttribute : Attribute
    {
        public string Tip { get; private set; }
        public TipAttribute(string tip)
        {
            Tip = tip;
        }
        
    }
}