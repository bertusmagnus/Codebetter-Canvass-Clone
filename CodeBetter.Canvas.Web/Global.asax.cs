
namespace CodeBetter.Canvas.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Canvas.Components;
    using Helpers;
    using Ninject;
    using Ninject.Web.Mvc;
    using Validation;

    public class MvcApplication :  NinjectHttpApplication 
    {
        public override void Init()
        {
            Error += OnError;
            EndRequest += OnEndRequest;
            base.Init();
        }    

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default","{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });
        }

        protected override void OnApplicationStarted()
        {
            RegisterAllControllersIn("CodeBetter.Canvas.Web");
         
            log4net.Config.XmlConfigurator.Configure();
            RegisterRoutes(RouteTable.Routes);

            Factory.Load(new Components.WebDependencies());
            ModelBinders.Binders.DefaultBinder = new Binders.GenericBinderResolver(Factory.TryGet<IModelBinder>);

            ValidatorConfiguration.Initialize("CodeBetter.Canvas");
            HtmlValidationExtensions.Initialize(ValidatorConfiguration.Rules);
        }

        private void OnEndRequest(object sender, System.EventArgs e)
        {
            if (((HttpApplication)sender).Context.Handler is MvcHandler)
            {
                CreateKernel().Get<ISessionSource>().Close();
            }
        }
        private void OnError(object sender, System.EventArgs e)
        {
            CreateKernel().Get<ISessionSource>().Close();
        }

        protected override IKernel CreateKernel()
        {
            return Factory.Kernel;
        }
    }    
}