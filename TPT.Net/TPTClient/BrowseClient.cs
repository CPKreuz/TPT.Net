using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TPT
{
    public partial class TPTClient
    {
        /// <summary>
        /// Get saves made by the logged in user
        /// </summary>
        /// <param name="count">The amount of save infos that should be returned</param>
        /// <param name="start">The index of the first save</param>
        public async Task<BrowseResult> GetOwnSaves(int count = 20, int start = 0)
        {
            return await GetBrowseResult("", start, count, BrowseCategory.ByOwn(), SortMode.Votes);
        }

        /// <summary>
        /// Get saves made by the logged in user
        /// </summary>
        /// <param name="sortMode">The applied save mode</param>
        /// <param name="count">The amount of save infos that should be returned</param>
        /// <param name="start">The index of the first save</param>
        public async Task<BrowseResult> GetOwnSaves(SortMode sortMode, int count = 20, int start = 0)
        {
            return await GetBrowseResult("", start, count, BrowseCategory.ByOwn(), sortMode);
        }

        /// <summary>
        /// Get the first page you get when opening TPT
        /// </summary>
        /// <param name="count">The max count of Save Infos, max: 30</param>
        public async Task<BrowseResult> GetFrontPage(int count = 20)
        {
            return await GetBrowseResult("", 0, count, null, SortMode.Votes);
        }

        /// <summary>
        /// Search for Saves by their name
        /// </summary>
        public async Task<BrowseResult> GetBrowseResultAsync(string searchQuery)
        {
            return await GetBrowseResult(searchQuery, 0, 20, null, SortMode.Votes);
        }

        /// <summary>
        /// Get Saves with the specified search mode
        /// </summary>
        public async Task<BrowseResult> GetBrowseResultAsync(SortMode sort)
        {
            return await GetBrowseResult("", 0, 20, null, sort);
        }

        /// <summary>
        /// Filter Saves by a category
        /// </summary>
        public async Task<BrowseResult> GetBrowseResultAsync(BrowseCategory category)
        {
            return await GetBrowseResult("", 0, 20, category, SortMode.Votes);
        }

        /// <summary>
        /// Search for saves
        /// </summary>
        /// <param name="searchQuery">The Name of the save</param>
        /// <param name="start">The index of the save you want to start with</param>
        /// <param name="count">The amount of saves you want to retrieve</param>
        /// <param name="category">The Category you want to filter by</param>
        /// <param name="sort">The sort mode you want to apply</param>
        public async Task<BrowseResult> GetBrowseResultAsync(string searchQuery = "", int start = 0, int count = 20, BrowseCategory category = null, SortMode sort = SortMode.Votes)
        {
            return await GetBrowseResult(searchQuery, start, count, category, sort);
        }

        private async Task<BrowseResult> GetBrowseResult(string searchQuery, int start, int count, BrowseCategory category, SortMode sort)
        {
            string categoryS = null;

            if (category != null)
            {
                categoryS = category.category;

                if (category.own)
                {
                    categoryS += LoggedInUsername;
                }
            }

            if (sort == SortMode.Date)
            {
                searchQuery += " sort:date";
            }

            HttpWebRequest request = BrowseResult.CreateRequest(searchQuery, categoryS, start, count, APIUrl, UserAgent);

            string response;

            using (WebResponse webResponse = await request.GetResponseAsync())
            {
                using (StreamReader stream = new StreamReader(webResponse.GetResponseStream()))
                {
                    response = stream.ReadToEnd();
                }
            }

            BrowseResult result = BrowseResult.Parse(response);

            result.client = this;
            result.URL = request.RequestUri.ToString();

            return result;
        }
    }
}
