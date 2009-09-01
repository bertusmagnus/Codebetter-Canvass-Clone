namespace CodeBetter.Canvas.Tests
{
    using FluentNHibernate;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using FluentNHibernate.Testing;
    using Mapping.Conventions;

    public class DatabaseFixture : BaseFixture
    {       
        public ISessionSource SessionSource{ get; private set;}
       
        public DatabaseFixture()
        {
            var configuration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory())
                .Mappings(m => m.FluentMappings.Conventions.AddFromAssemblyOf<TableNameConvention>());

            SessionSource = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration.BuildConfiguration().Properties, new TestModel());
            SessionSource.BuildSchema();
        }

        public class TestModel : PersistenceModel
        {
            public TestModel()
            {
                AddMappingsFromAssembly(typeof(User).Assembly);
            }
        }
    }
}