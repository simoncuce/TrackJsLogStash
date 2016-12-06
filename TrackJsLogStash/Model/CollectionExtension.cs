using System.Collections.Generic;

namespace TrackJsLogStash.Model
{
    public static class CollectionExtension
    {
        public static string ToOutput(this List<Metadata> metaDataList)
        {

            if (metaDataList == null || metaDataList.Count == 0)
            { 
                return "";
            }

            var result = "";

            foreach (var metadata in metaDataList)
            {
                result += (!string.IsNullOrEmpty(metadata.key) ? metadata.key : "") + "," +
                          (!string.IsNullOrEmpty(metadata.value) ? metadata.value : "");
            }

            return result;

        }
    }
}