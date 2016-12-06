using System.Collections.Generic;

namespace TrackJsLogStash.Model
{
    public class Data
    {
        public string message { get; set; }
        public string timestamp { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public string browserName { get; set; }
        public string browserVersion { get; set; }
        public string entry { get; set; }
        public int line { get; set; }
        public int column { get; set; }
        public string file { get; set; }
        public string userId { get; set; }
        public string sessionId { get; set; }
        public string trackJsUrl { get; set; }
        public List<string> stackTrace { get; set; }
        public List<Metadata> metadata { get; set; }
    }
}