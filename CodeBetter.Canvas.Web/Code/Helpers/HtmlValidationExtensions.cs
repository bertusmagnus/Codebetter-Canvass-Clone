namespace CodeBetter.Canvas.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;
    using Validation;
    
    public static class HtmlValidationExtensions
    {
        private static readonly IDictionary<Type, string> _rules = new Dictionary<Type, string>();

        public static string RulesFor<T>(this HtmlHelper html)
        {
            return html.RulesFor<T>(string.Empty);
        }
        public static string RulesFor<T>(this HtmlHelper html, string prefix)
        {
            var replacement = string.IsNullOrEmpty(prefix) ? string.Empty : string.Concat(prefix, '_');
            return _rules[typeof(T)].Replace("#PREFIX#", replacement);
        }        
        public static string[] GetPageErrors(this HtmlHelper html)
        {
            var errors = (ICollection<ValidationError>)html.ViewData["ValidationErrors"];
            if (errors == null)
            {
                return null;
            }
            return errors.Map(e => e.Field == null ? e.Message : null);
        }

        public static string GetValidationErrors(this HtmlHelper html)
        {
            var errors = (ICollection<ValidationError>)html.ViewData["ValidationErrors"];
            if (errors == null)
            {
                return "init:{}";                    
            }
            var sb = new StringBuilder("init:{");
            errors.Each(e => 
                {
                    if (e.Field != null)
                    {
                        sb.AppendFormat("{0}:'{1}',", e.Field, html.Js(e.Message));
                    }
                });
            return sb.RemoveLastIf(',').Append("}").ToString();                        
        }

        public static void Initialize(IDictionary<Type, EntityValidationInfo> rules)
        {
            foreach (var rule in rules)
            {
                _rules.Add(rule.Key, BuildJson(rule.Value));
            }
        }
        private static string BuildJson(EntityValidationInfo entity)
        {
            var sb = new StringBuilder("rules:{");
            foreach (var property in entity.Properties)
            {
                SerializeProperty(string.Empty, property, sb);
            }
            return sb.RemoveLast().Append("}").ToString();
        }
        private static void SerializeProperty(string chain, PropertyValidationInfo property, StringBuilder sb)
        {
            chain += property.Property.Name;
            if (property.Component == null)
            {
                Serialize(chain, property, sb);
                return;
            }
            chain += '_';
            foreach (var p in property.Component.Properties)
            {
                SerializeProperty(chain, p, sb);
            }
        }
        private static void Serialize(string name, PropertyValidationInfo property, StringBuilder sb)
        {
            sb.Append("#PREFIX#");
            sb.Append(name);
            sb.Append(":{");
            foreach (var attribute in property.Validators)
            {
                if (attribute is ValidateComponentAttribute)
                {
                    continue;
                }
                foreach (var kvp in attribute.ToProperties())
                {
                    sb.AppendFormat("{0}:{1},", kvp.Key, kvp.Value);
                }
            }
            var tip = property.Property.GetCustomAttributes(typeof(TipAttribute), true);
            if (tip != null && tip.Length > 0)
            {
                sb.AppendFormat("tip:'{0}'", (((TipAttribute)tip[0]).Tip).Replace("'", "\\'"));
            }
            sb.RemoveLastIf(',');
            sb.Append("},");
        }
    }
}