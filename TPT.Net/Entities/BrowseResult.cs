using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using TPT;

namespace TPT
{
    public class BrowseResult : Request<BrowseResult>
    {
        internal string URL;

        /// <summary>
        /// The Total amount of pages
        /// </summary>
        [JsonProperty("Count")]
        public int TotalPages { get; internal set; }

        [JsonProperty(PropertyName = "Saves")]
        internal SaveInfo[] saves;

        /// <summary>
        /// Saves found (These save infos do not include Tags, you can get the Tags by using <see cref="SaveInfo.Update"/>
        /// </summary>
        [JsonIgnore]
        public IReadOnlyCollection<SaveInfo> Saves => saves;

        public SaveInfo this[int i] => saves[i];

        internal static HttpWebRequest CreateRequest(string searchQuery, string category, int start, int count, string api, string userAgent)
        {
            string url = api + "Browse.json?Count=" + count + "&Start=" + start;

            if (searchQuery != null && !string.IsNullOrWhiteSpace(searchQuery))
            {
                url += "&Search_Query=" + HttpUtility.UrlEncode(searchQuery);
            }

            if (category != null)
            {
                url += "&Category=" + HttpUtility.UrlEncode(category);
            }

            HttpWebRequest request = WebRequest.CreateHttp(url);

            request.UserAgent = userAgent;

            return request;
        }
    }
}
