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
        public async Task<UserInfo> GetUserAsync(string username)
        {
            HttpWebRequest request = UserInfo.CreateRequest(username, APIUrl, UserAgent);

            string response;

            using (WebResponse webResponse = await request.GetResponseAsync())
            {
                using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
            }

            HttpStatusCode code = ParseResult(response);

            CheckStatusCode(code, username);

            return UserInfo.Parse(response);
        }

        private HttpStatusCode ParseResult(string response)
        {
            // Note: I did not write the endpoint
            // and the person who did must have been drunk

            if (response.StartsWith("Error:"))
            {
                return (HttpStatusCode)Convert.ToInt32(response.Substring(7, 3));
            }

            return HttpStatusCode.OK;
        }

        private void CheckStatusCode(HttpStatusCode code, string username)
        {
            if (code == HttpStatusCode.NotFound) throw new UsernotFoundException(username);
            else if (code != HttpStatusCode.OK) throw new WebException("Server responded with " + (int)code + " " + code);
        }
    }
}
