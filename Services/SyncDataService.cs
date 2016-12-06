using System;

namespace TrackJsLogStash.Services
{
    public class SyncDataService
    {
        public void UpdateLastDate(DateTime endDate)
        {
            Properties.Settings.Default["LastUpdatedDate"] = endDate.ToString("o");
            Properties.Settings.Default.Save();
        }

        public DateTime GetLastDate()
        {
            try
            {
                var result = Properties.Settings.Default["LastUpdatedDate"];

                return Convert.ToDateTime(result);
            }
            catch (Exception)
            {
                return DateTime.Now.AddMinutes(-1);
            }

        }
    }
}
