namespace CodeBetter.Canvas.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Validation;

    public class ApplicationController : Controller
    {
        private static readonly string _applicationPath = System.Web.HttpContext.Current == null ? string.Empty : System.Web.HttpContext.Current.Request.ApplicationPath == "/" ? string.Empty : System.Web.HttpContext.Current.Request.ApplicationPath;
        internal static string ApplicationPath
        {
            get { return _applicationPath; }
        }

        public virtual User CurrentUser { get { return null; } }

        public static RedirectResult RedirectTo<C>(Expression<Func<C, ActionResult>> action)
        {
            return RedirectTo(action, null);
        }
        public static RedirectResult RedirectTo<C>(Expression<Func<C, ActionResult>> action, string query)
        {
            return new RedirectResult(LinkTo(action, query));
        }
        public static RedirectResult RedirectToLogin()
        {
            return RedirectTo<HomeController>(c => c.Index());
        }
        public static string LinkTo<T>(Expression<Func<T, ActionResult>> action)
        {
            return LinkTo(action, null);
        }
        public static string LinkTo<T>(Expression<Func<T, ActionResult>> action, string query)
        {
            var body = action.Body as MethodCallExpression;
            if (body == null)
            {
                throw new InvalidOperationException("Expression must be a method call");
            }
            var controller = typeof(T).Name.Replace("Controller", string.Empty);
            var url = string.Format("{0}/{1}/{2}", _applicationPath, controller, body.Method.Name);
            if (query != null)
            {
                url += "?" + query;
            }
            return url;
        }

        internal void AddError(params ValidationError[] e)
        {
            if (e == null || e.Length == 0)
            {
                return;
            }

            var errors = (ICollection<ValidationError>)ViewData["ValidationErrors"];
            if (errors == null)
            {
                errors = new List<ValidationError>(e.Length);
                ViewData["ValidationErrors"] = errors;
                ModelState.AddModelError(string.Empty, string.Empty); //HACK to set IsValid to false
            }
            Array.ForEach(e, errors.Add);
        }
    }
}