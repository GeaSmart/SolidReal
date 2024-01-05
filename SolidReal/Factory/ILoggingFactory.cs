using SolidReal.Logging;

namespace SolidReal.Factory
{
    public interface ILoggingFactory
    {
        ILogging GetLogger();
    }
}