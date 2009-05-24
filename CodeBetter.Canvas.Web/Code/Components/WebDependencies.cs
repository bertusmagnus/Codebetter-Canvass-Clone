namespace CodeBetter.Canvas.Web.Components
{
    using Binders;
    using Ninject.Modules;

    public class WebDependencies : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthenticationManager>().To<AuthenticationManager>().InRequestScope();
            Bind<ModelBinder<User>>().To<UserBinder>().InRequestScope();
        }
    }
}