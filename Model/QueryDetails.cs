namespace TrackJsLogStash.Model
{
    public class QueryDetails
    {
        public string startDate { get; set; }
        public int totalCount { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public bool hasMore { get; set; }
        public string trackJsUrl { get; set; }
    }
}