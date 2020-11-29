using System;
using System.Collections.Generic;
using System.Text;

namespace TPT
{
    public class BrowseCategory
    {
        internal string category;
        internal bool own;

        internal BrowseCategory(string category)
        {
            this.category = category;
        }

        internal BrowseCategory(string category, bool own)
        {
            this.category = category;
            this.own = own;
        }

        /// <summary>
        /// Filter saves by logged in users favourites
        /// </summary>
        public static BrowseCategory Favourites() => new BrowseCategory("&Category=Favourites");

        /// <summary>
        /// Filter saves made by <paramref name="username"/>
        /// </summary>
        public static BrowseCategory ByUser(string username) => new BrowseCategory("&Category=by:" + username);

        /// <summary>
        /// Filter saves made by logged in user
        /// </summary>
        public static BrowseCategory ByOwn() => new BrowseCategory("&Category=by:", true);

        /// <summary>
        /// You could also just use null
        /// </summary>
        public static BrowseCategory Nothing() => null;
    }
}
