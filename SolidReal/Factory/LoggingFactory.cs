using MySolidAPI;
using SolidReal.Logging;

namespace SolidReal.Factory
{
    public class LoggingFactory : ILoggingFactory
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        public LoggingFactory(IConfiguration configuration, ApplicationDbContext context)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public ILogging GetLogger()
        {
            var type = configuration["LoggingType"];
            string className = string.Format("{0}.Logging.{1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, type);

            return (ILogging)Activator.CreateInstance(Type.GetType(className), args: new object[] { context });
        }
    }
}
