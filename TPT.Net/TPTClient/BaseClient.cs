using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TPT.Exceptions;

namespace TPT
{
    /// <summary>
    /// A Client for interacting with the TPT Api
    /// </summary>
    public partial class TPTClient
    {
        internal const string APIUrl = "https://powdertoy.co.uk/";
        internal const string StaticAPIUrl = "https://static.powdertoy.co.uk/";
        internal static readonly JsonConverter[] converters = { new UnixTimestampConverter() };
        public string UserAgent = "TPT.Net";

        /// <summary>
        /// Gets the Username of the Logged in user
        /// </summary>
        public string LoggedInUsername { get; private set; }
    }
}
