namespace Assets.Cube_Loader.src
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    public static class Helpers
    {

        public static WWW GetConfiguredWww(string url, string staticToken = null, bool requestCompression = true)
        {
            var headers = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(staticToken))
            {
                headers.Add("Authorization", "StaticToken " + staticToken);
            }

            if (requestCompression)
            {
                headers.Add("Accept-Encoding", "gzip, deflate");
            }
            
            return new WWW(url, null, headers);
        }
    }
}
