namespace Assets.Cube_Loader.src
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using ICSharpCode.SharpZipLib.GZip;
    using UnityEngine;
    using Debug = UnityEngine.Debug;

    public static class Extensions
    {
        private const string ContentEncodingHeaderName = "CONTENT-ENCODING";
        private const string GzipContentEncodingValue = "gzip";

        public static string GetUnzippedText(this WWW response)
        {
            string contentEncoding;
            if (response.responseHeaders == null ||
                !response.responseHeaders.TryGetValue(ContentEncodingHeaderName, out contentEncoding) ||
                contentEncoding != GzipContentEncodingValue)
            {
                return response.text;
            }

            using (var stream = new MemoryStream(response.bytes))
            using (var gzip = new GZipInputStream(stream))
            using (var sr = new StreamReader(gzip))
            {
                return sr.ReadToEnd();
            }
        }

        public static void LogResponseIfError(this WWW response)
        {
            if (!string.IsNullOrEmpty(response.error))
            {
                Debug.LogErrorFormat("Error getting {0}: {1}", response.url, response.error);
            }
        }
    }
}
