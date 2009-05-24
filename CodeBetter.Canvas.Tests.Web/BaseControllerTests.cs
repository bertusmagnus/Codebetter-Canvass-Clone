namespace CodeBetter.Canvas.Tests.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Validation;
    using Xunit;

    public class BaseControllerTests : BaseFixture
    {

        public static class MvcAssert
        {
            public static void DefaultView(ActionResult result)
            {
                ViewName(string.Empty, result);
            }
            public static void DefaultViewWithModel(object model, ActionResult result)
            {
                ViewNameWithModel(string.Empty, model, result);
            }
            public static void ViewName(string expected, ActionResult result)
            {
                Assert.Equal(expected, ((ViewResult)result).ViewName);
            }
            public static void ViewNameWithModel(string expected, object model, ActionResult result)
            {
                var view = (ViewResult)result;
                ViewName(expected, result);
                if (model is ValueType)
                {
                    Assert.Equal(model, view.ViewData.Model);
                }
                else
                {
                    Assert.Same(model, view.ViewData.Model);
                }
            }

            public static void RedirectTo(string expected, ActionResult result)
            {
                var redirect = (RedirectResult)result;
                Assert.Equal(expected, redirect.Url);
            }

            public static void HasError(ActionResult result, int index, string field, string message)
            {
                var view = (ViewResult)result;
                var error = ((IList<ValidationError>)view.ViewData["ValidationErrors"])[index];
                Assert.Equal(field, error.Field);
                Assert.Equal(message, error.Message);
            }
        }
    }
}