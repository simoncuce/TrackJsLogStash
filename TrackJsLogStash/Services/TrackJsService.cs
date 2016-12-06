using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using TrackJsLogStash.Model;

namespace TrackJsLogStash.Services
{
    public class TrackJsService
    {
        private readonly string _url = "https://api.trackjs.com/";

        public void GetData(string clientId, string apiKey, string application, DateTime startDate, DateTime endDate, Action<Data> upload)
        {
           
            var counter = 1;
            bool doNext;

            do
            {
                var response = JsonConvert.DeserializeObject<TrackJsResponse>(GetJson(clientId, apiKey, application, startDate, endDate, counter));

                Parallel.ForEach(response.data, upload); //make async

                counter++;

                doNext = response.metadata.hasMore;

            } while (doNext);


        }

        private string GetJson(string clientId, string apiKey, string application, DateTime startDate, DateTime endDate, int page = 1)
        {
            var serviceAddress = CreateServiceURL(clientId, apiKey, application, startDate, endDate, page);

            using (var request = CreateRequest(_url, serviceAddress, "application/json", HttpMethod.Get))
            {
                using (var client = new HttpClient())
                {
                    var response = client.SendAsync(request).Result;

                    return response.Content.ReadAsStringAsync().Result;
                }
            }

        }

        private string CreateServiceURL(string clientId, string apiKey, string application, DateTime startDate, DateTime endDate, int page)
        {
            var pageSize = 100;

            var serviceAddress = $"{clientId}/v1/errors?";
           
            var startDateString = HttpUtility.UrlEncode(startDate.ToString("o"));
            var endDateString = HttpUtility.UrlEncode(endDate.ToString("o"));

            serviceAddress += $"startDate={startDateString}&endDate={endDateString}";
            serviceAddress += $"&size={pageSize}";
            serviceAddress += $"&key={apiKey}";
            serviceAddress += $"&page={page}";

            if (!string.IsNullOrEmpty(application))
                serviceAddress += $"&application={application}";

            return serviceAddress;
        }

        private HttpRequestMessage CreateRequest(string baseUrl, string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage {RequestUri = new Uri(baseUrl + url)};

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }
    }
}