using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TPT.Exceptions;

namespace TPT
{
    public partial class TPTClient
    {
        /// <summary>
        /// Download the save file
        /// </summary>
        /// <param name="id">Save ID</param>
        /// <exception cref="SaveNotFoundException"/>
        public async Task<Stream> DownloadSaveAsync(int id)
        {
            HttpWebRequest request = WebRequest.CreateHttp(StaticAPIUrl + id + ".cps");

            request.UserAgent = UserAgent;

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

            CheckStatusCode(response.StatusCode, id);

            return response.GetResponseStream();
        }

        /// <summary>
        /// Download the save file
        /// </summary>
        /// <param name="id">Save ID</param>
        /// <param name="time">Creation Date</param>
        /// <exception cref="SaveNotFoundException"/>
        public async Task<Stream> DownloadSaveAsync(int id, DateTime time)
        {
            HttpWebRequest request = WebRequest.CreateHttp(StaticAPIUrl + id + "_" + time.ToUnixtime() + ".cps");

            request.UserAgent = UserAgent;

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

            CheckStatusCode(response.StatusCode, id);

            return response.GetResponseStream();
        }

        private void CheckStatusCode(HttpStatusCode code, int id)
        {
            if (code == HttpStatusCode.NotFound) throw new SaveNotFoundException(id);
            else if (code != HttpStatusCode.OK) throw new WebException("Server responded with " + (int)code + " " + code);
        }
    }
}
