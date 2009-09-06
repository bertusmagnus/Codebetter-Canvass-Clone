namespace CodeBetter.Canvas.Web
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Canvas.Components;
    using Ninject;
    using Ninject.Web.Mvc;
    using Spark;
    using Spark.Web.Mvc;
    using Validation;

    public class MvcApplication :  NinjectHttpApplication 
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default","{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = "" });
        }        
        protected override void OnApplicationStarted()
        {
            Error += OnError;
            EndRequest += OnEndRequest;
            
             var settings = new SparkSettings()
                .AddNamespace("System")
                .AddNamespace("System.Collections.Generic")
                .AddNamespace("System.Web.Mvc")
                .AddNamespace("System.Web.Mvc.Html")
                .AddNamespace("MvcContrib.FluentHtml")
                .AddNamespace("CodeBetter.Canvas")
                .AddNamespace("CodeBetter.Canvas.Web")
                .SetPageBaseType("ApplicationViewPage")
                .SetAutomaticEncoding(true);
                               
#if DEBUG
            settings.SetDebug(true);
#endif
            var viewFactory = new SparkViewFactory(settings);
            ViewEngines.Engines.Add(viewFactory);
#if !DEBUG            
            PrecompileViews(viewFactory);
#endif            

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
        
        private static void PrecompileViews(SparkViewFactory viewFactory)
        {
            var batch = new SparkBatchDescriptor();
            batch.For<HomeController>().For<ManageController>();
            viewFactory.Precompile(batch);                  
        }
    }    
}