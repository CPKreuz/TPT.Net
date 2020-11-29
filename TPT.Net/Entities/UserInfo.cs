using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TPT
{
    public class UserInfo : Request<UserInfo>
    {
        /// <summary>
        /// The name of this user
        /// </summary>
        [JsonProperty]
        public string Username { get; internal set; }
        /// <summary>
        /// The ID of this user
        /// </summary>
        [JsonProperty]
        public int ID { get; internal set; }
        /// <summary>
        /// The avatar URL of this user
        /// </summary>
        [JsonProperty]
        public string Avatar { get; internal set; }
        /// <summary>
        /// The age of this user
        /// </summary>
        [JsonProperty]
        public int? Age { get; internal set; }
        /// <summary>
        /// The biography of this user
        /// </summary>
        [JsonProperty]
        public string Biography;
        /// <summary>
        /// The website of this user
        /// </summary>
        [JsonProperty]
        public string Website { get; internal set; }
        /// <summary>
        /// When the user registered
        /// </summary>
        [JsonProperty]
        public DateTime RegisterTime { get; internal set; }

        /// <summary>
        /// Info about the saves of the user
        /// </summary>
        [JsonProperty]
        public UsersSaves Saves { get; internal set; }
        /// <summary>
        /// Info about the activity of this user on the forum
        /// </summary>
        [JsonProperty]
        public UsersForumData Forum { get; internal set; }

        /// <summary>
        /// Get all saves made by this user
        /// </summary>
        public Task<BrowseResult> GetSaves()
        {
            return client.GetBrowseResultAsync(BrowseCategory.ByUser(Username));
        }

        public struct UsersSaves
        {
            /// <summary>
            /// Total amount of public saves this user made
            /// </summary>
            public int Count;
            /// <summary>
            /// The average score of all public saves
            /// </summary>
            public float AverageScore;
            /// <summary>
            /// The highest score of all public saves
            /// </summary>
            public int HighestScore;
        }

        public struct UsersForumData
        {
            /// <summary>
            /// Total amount of topics this user opened
            /// </summary>
            public int Topics;
            /// <summary>
            /// Total amount of replies this user made
            /// </summary>
            public int Replies;
            /// <summary>
            /// Reputation of this user on the forum
            /// </summary>
            public int Reputation;
        }

        internal static HttpWebRequest CreateRequest(string username, string api, string userAgent)
        {
            HttpWebRequest request = WebRequest.CreateHttp(api + "User.json?Name=" + username);

            request.UserAgent = userAgent;

            return request;
        }
    }
}
