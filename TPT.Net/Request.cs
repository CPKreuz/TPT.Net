using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TPT
{
    public abstract class Request<T>
    {
        internal TPTClient client;

        internal static T Parse(string json)
        {
            if (typeof(T) == typeof(UserInfo))
            {
                json = json.Substring(8, json.Length - 9);
            }

            return JsonConvert.DeserializeObject<T>(json, TPTClient.converters);
        }
    }
}
