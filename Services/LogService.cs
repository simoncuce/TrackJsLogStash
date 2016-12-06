using NLog;

namespace TrackJsLogStash.Services
{
    public static class LogService
    {

        private static readonly Logger LoggerClass = LogManager.GetCurrentClassLogger();

        public static Logger Logger()
        {
            return LoggerClass;
        }
    }
}