using System.Collections.Generic;

namespace TrackJsLogStash.Model
{
    public class TrackJsResponse
    {
        public List<Data> data { get; set; }
        public QueryDetails metadata { get; set; }
    }
}