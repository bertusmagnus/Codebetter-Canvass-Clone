namespace CodeBetter.Canvas
{
    using System.Configuration;
    using System.Web;
    using System.Xml;
    
    //HACK!!!!
    //This is the one really-bad thing in this sample application - I did this so that the
    //sample would run-out of the box (no need to configure a database or connection string).
    //It's horrible. Sorry.
    public class Configuration
    {
        private static readonly Configuration _configuration = (Configuration)ConfigurationManager.GetSection("CodeBetter.Canvas/Web");
        private readonly string _connectionString;

        public static Configuration GetConfig()
        {
            return _configuration;
        }
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public Configuration(XmlNode node)
        {
            if (node == null)
            {
                throw new ConfigurationErrorsException("Missing configuration CodeBetter.Canvas/Web");
            }
            _connectionString = node.GetString("connectionString");
#if DEBUG
            _connectionString = MakeRelative(_connectionString);      
        }
        private static string MakeRelative(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.StartsWith("~") && HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(value);
            }
            return value;
        }
#endif
    }

    public class ConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            return new Configuration(section);
        }
    }
}
