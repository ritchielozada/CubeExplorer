namespace Assets.Cube_Loader.src
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using ICSharpCode.SharpZipLib.GZip;
    using UnityEngine;
    using Debug = UnityEngine.Debug;
    using DeflateStream = Ionic.Zlib.DeflateStream;
    using GZipStream = Ionic.Zlib.GZipStream;

    public static class Extensions
    {
        private const string ContentEncodingHeaderName = "CONTENT-ENCODING";
        private const string GzipContentEncodingValue = "gzip";

        public static string GetDecompressedText(this WWW response)
        {
            string contentEncoding;
            if (response.responseHeaders == null ||
                !response.responseHeaders.TryGetValue(ContentEncodingHeaderName, out contentEncoding) ||
                contentEncoding != GzipContentEncodingValue)
            {
                return response.text;
            }
            return GZipStream.UncompressString(response.bytes);
        }

        public static byte[] GetDecompressedBuffer(this WWW response)
        {
            string contentEncoding;
            if (response.responseHeaders == null ||
                !response.responseHeaders.TryGetValue(ContentEncodingHeaderName, out contentEncoding) ||
                contentEncoding != GzipContentEncodingValue)
            {
                return response.bytes;
            }
            return GZipStream.UncompressBuffer(response.bytes);
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
