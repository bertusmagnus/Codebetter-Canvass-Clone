namespace CodeBetter.Canvas.Web.Helpers
{
    using System;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    
    public static class HtmlExtensions
    {
        private static readonly string _applicationPath = HttpContext.Current.Request.ApplicationPath == "/" ? string.Empty : HttpContext.Current.Request.ApplicationPath;

        public static string IncludeCss(this HtmlHelper html, string name)
        {
            return string.Format("<link href=\"{0}/Content/styles/{1}.css\" rel=\"stylesheet\" type=\"text/css\"/>", _applicationPath, name);
        }

        public static string IncludeJs(this HtmlHelper html, string name)
        {
               
            return string.Format("<script src=\"{0}/Content/js/{1}.js\" type=\"text/javascript\"></script>", _applicationPath, name);
        }

        public static string Image(this HtmlHelper html, string name, int width, int height, string alt)
        {
            return string.Format("<img src=\"{0}/content/images/{1}\" width=\"{2}\" height=\"{3}\" alt=\"{4}\" />", _applicationPath, name, width, height, alt);           
        }

        public static string LinkTo<T>(this HtmlHelper html, Expression<Func<T, ActionResult>> action)
        {            
            var body = action.Body as MethodCallExpression;
            if (body == null)
            {
                throw new InvalidOperationException("Expression must be a method call");
            }                           
            var controller = typeof(T).Name.Replace("Controller", string.Empty);
            return string.Format("{0}/{1}/{2}", _applicationPath, controller, body.Method.Name);
        }

        public static string Js(this HtmlHelper html, string @string)
        {

            return @string == null ? string.Empty : @string.Replace("'", "\\'");
        }
    }
}