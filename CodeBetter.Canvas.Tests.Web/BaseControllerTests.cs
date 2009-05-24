namespace CodeBetter.Canvas.Tests.Web
{
    using System.Web.Mvc;
    using Xunit;

    public class BaseControllerTests : BaseFixture
    {
        
        public static class MvcAssert
        {
            public static void DefaultView(ViewResult result)
            {
                ViewName(string.Empty, result);
            }
            public static void DefaultViewWithModel(object model, ViewResult result)
            {
                ViewNameWithModel(string.Empty, model, result);
            }
            public static void ViewName(string expected, ViewResult result)
            {
                Assert.Equal(expected, result.ViewName);
            }
            public static void ViewNameWithModel(string expected, object model, ViewResult result)
            {
                ViewName(expected, result);
                Assert.Same(model, result.ViewData.Model);
            }            
        }
    }
}