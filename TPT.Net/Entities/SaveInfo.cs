using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TPT
{
    public class SaveInfo : Request<SaveInfo>
    {
        /// <summary>
        /// The Save ID
        /// </summary>
        [JsonProperty]
        public int ID { get; internal set; }
        /// <summary>
        /// Did the logged in user mark this as favourite
        /// </summary>
        [JsonProperty]
        public bool Favourite { get; internal set; }
        /// <summary>
        /// The amount of upvotes minus the amount of downvotes
        /// </summary>
        [JsonProperty]
        public int Score { get; internal set; }
        /// <summary>
        /// The amount of upvotes a got got
        /// </summary>
        [JsonProperty]
        public int ScoreUp { get; internal set; }
        /// <summary>
        /// The amount of downvotes a save got
        /// </summary>
        [JsonProperty]
        public int ScoreDown { get; internal set; }
        /// <summary>
        /// The percantage of upvotes
        /// </summary>
        public int ScoreRatio => 100 - ScoreDown / ScoreUp * 100;
        /// <summary>
        /// The amount of Views a save got
        /// </summary>
        [JsonProperty]
        public int Views { get; internal set; }
        /// <summary>
        /// The name of the Save
        /// </summary>
        [JsonProperty]
        public string Name { get; internal set; }
        /// <summary>
        /// The description of the Save
        /// </summary>
        [JsonProperty]
        public string Description { get; internal set; }
        /// <summary>
        /// When the Save was created
        /// </summary>
        [JsonProperty]
        public DateTime DateCreated { get; internal set; }
        /// <summary>
        /// When this <see cref="Version"/> was created
        /// </summary>
        [JsonProperty(PropertyName = "Date")]
        public DateTime DateModified { get; internal set; }
        /// <summary>
        /// The name of the Author that created the save
        /// </summary>
        [JsonProperty(PropertyName = "Username")]
        public string AuthorName { get; internal set; }
        /// <summary>
        /// The amount of comments posted on this save
        /// </summary>
        [JsonProperty]
        public int Comments { get; internal set; }
        /// <summary>
        /// Is this save findable when searching using <see cref="TPTClient.GetBrowseResultAsync(string)"/>
        /// </summary>
        [JsonProperty]
        public bool Published { get; internal set; }
        /// <summary>
        /// 0 when the newest version, otherwise the same as <see cref="DateModified"/> in Unix Timestamp
        /// </summary>
        [JsonProperty]
        public long Version { get; internal set; }

        /// <summary>
        /// The tags of this save
        /// </summary>
        [JsonProperty]
        public string[] Tags { get; internal set; }

        /// <summary>
        /// Is this last modified version of the save
        /// </summary>
        public bool IsNewestVersion() => Version == 0;

        /// <summary>
        /// Was this save modified
        /// </summary>
        public bool IsModified() => DateCreated != DateModified;

        /// <summary>
        /// Returns the name of the save
        /// </summary>
        public override string ToString() => Name;

        /// <summary>
        /// Updates all values
        /// </summary>
        public async Task Update()
        {
            SaveInfo tmp = await client.GetSaveInfoAsync(ID, DateModified);

            ID = tmp.ID;
            Favourite = tmp.Favourite;
            Score = tmp.Score;
            ScoreUp = tmp.ScoreUp;
            ScoreDown = tmp.ScoreDown;
            Views = tmp.Views;
            Name = tmp.Name;
            Description = tmp.Description;
            DateCreated = tmp.DateCreated;
            DateModified = tmp.DateModified;
            AuthorName = tmp.AuthorName;
            Comments = tmp.Comments;
            Published = tmp.Published;
            Version = tmp.Version;
            Tags = tmp.Tags;
        }

        public Task<UserInfo> GetAuthorAsync()
        {
            return client.GetUserAsync(AuthorName);
        }

        /// <summary>
        /// Get the URL to the save
        /// </summary>
        /// <param name="newest">The link to the always newest version</param>
        public string GetDownloadUrl(bool newest)
        {
            if (newest)
            {
                return TPTClient.StaticAPIUrl + ID + ".cps";
            }
            else
            {
                return TPTClient.StaticAPIUrl + ID + "_" + Version + ".cps";
            }
        }

        /// <summary>
        /// Download the save file
        /// </summary>
        /// <returns></returns>
        public Task<Stream> DownloadAsync()
        {
            return client.DownloadSaveAsync(ID);
        }

        /// <summary>
        /// Get's the newest version of the SaveInfo
        /// </summary>
        /// <returns></returns>
        public async Task<SaveInfo> GetNewest() => await client.GetSaveInfoAsync(ID, Extensions.FromUnixtime(0));

        internal static HttpWebRequest CreateRequest(int id, DateTime? date, string api, string userAgent)
        {
            string url = api + "Browse/View.json?ID=" + id;

            if (date != null)
            {
                url += "&Date=" + date.Value.ToUnixtime();
            }

            HttpWebRequest request = WebRequest.CreateHttp(url);

            request.UserAgent = userAgent;

            return request;
        }
    }
}
