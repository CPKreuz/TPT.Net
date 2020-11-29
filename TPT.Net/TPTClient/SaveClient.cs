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
        /// Gets a SaveInfo by it's ID
        /// </summary>
        /// <param name="id">The Save ID</param>
        /// <exception cref="SaveNotFoundException"/>
        public Task<SaveInfo> GetSaveInfoAsync(int id)
        {
            return GetSaveInfo(id, null);
        }

        /// <summary>
        /// Gets a SaveInfo by it's ID and creation Time
        /// </summary>
        /// <param name="id">The Save ID</param>
        /// <param name="date">The Date this version was created</param>
        /// <exception cref="SaveNotFoundException"/>
        public Task<SaveInfo> GetSaveInfoAsync(int id, DateTime date)
        {
            return GetSaveInfo(id, date);
        }

        private async Task<SaveInfo> GetSaveInfo(int id, DateTime? date)
        {
            HttpWebRequest request = SaveInfo.CreateRequest(id, date, APIUrl, UserAgent);

            string response;

            using (WebResponse webResponse = await request.GetResponseAsync())
            {
                using (StreamReader stream = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = stream.ReadToEnd();
                }
            }

            SaveInfo info = SaveInfo.Parse(response);

            // Check if save was found
            if (info.ID == 404 && id != 404) throw new SaveNotFoundException(id);

            // Set Description to null if no description was provided
            if (info.Description == "No Description provided.") info.Description = null;

            info.client = this;

            return info;
        }
    }
}
