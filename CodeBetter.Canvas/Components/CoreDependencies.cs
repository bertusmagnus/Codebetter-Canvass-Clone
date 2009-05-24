namespace CodeBetter.Canvas.Components
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Mapping.Conventions;
    using Ninject.Modules;    
    using Validation;

    public class CoreDependencies : NinjectModule
    {
        public override void Load()
        {
            var connectionString = string.Format("Data Source={0};Version=3;New=False;", Configuration.GetConfig().ConnectionString);
            var configuration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(c => c.Is(connectionString)))
                .Mappings(m => m.FluentMappings
                                .AddFromAssemblyOf<User>()
                                .ConventionDiscovery.AddFromAssemblyOf<TableNameConvention>());

            Bind<ISessionSource>()
                .To<SessionSource>()
                .InRequestScope()
                .WithConstructorArgument("config", configuration);

            Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            Bind<IValidator>().To<Validator>();
            Bind<IEncryptor>().To<Encryptor>();
        }
    }
}
