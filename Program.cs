using System;
using System.Configuration;
using NLog;
using TrackJsLogStash.Services;
using TrackJsLogStash.Model;

namespace TrackJsLogStash
{
    class Program
    {
        static void Main()
        {

            var syncDataService = new SyncDataService();
            var trackJsService = new TrackJsService();

            var clientId = ConfigurationManager.AppSettings["TrackJsClientId"];
            var apiKey = ConfigurationManager.AppSettings["TrackJsApiKey"];
            var application = ConfigurationManager.AppSettings["TrackJsApplication"];

            if (string.IsNullOrEmpty(clientId))
            {
                Console.WriteLine("Track JS client ID is missing from the configuration");
                return;
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("Track JS API Key is missing from the configuration");
                return;
            }

            var endDate = DateTime.Now;
            var startDate = syncDataService.GetLastDate();

            trackJsService.GetData(clientId, apiKey, application, startDate, endDate,
                data =>
                {
                    var logEvent = new LogEventInfo(LogLevel.Warn, "", data.message);

                    logEvent.Properties["messageDetails"] = data.message;
                    logEvent.Properties["timestamp"] = data.timestamp;
                    logEvent.Properties["messageId"] = data.id;
                    logEvent.Properties["browserName"] = data.browserName;
                    logEvent.Properties["broswerVersion"] = data.browserVersion;
                    logEvent.Properties["entry"] = data.entry;
                    logEvent.Properties["line"] = data.line;
                    logEvent.Properties["column"] = data.column;
                    logEvent.Properties["file"] = data.file;
                    logEvent.Properties["userId"] = data.userId;
                    logEvent.Properties["sessionId"] = data.sessionId;
                    logEvent.Properties["trackJsUrl"] = data.trackJsUrl;
                    logEvent.Properties["metadata"] = data.metadata.ToOutput();

                    LogService.Logger().Warn(logEvent);

                    Console.WriteLine($"Add {data.message}");
                });

            syncDataService.UpdateLastDate(endDate);

            Console.WriteLine("Track JS LogStash complete");

        }

    }
}
