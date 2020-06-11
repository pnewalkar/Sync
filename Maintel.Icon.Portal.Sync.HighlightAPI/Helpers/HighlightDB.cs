using System;  
using System.Net;
using System.Text;
using System.IO;
using Maintel.Icon.Portal.Sync.HighlightAPI.Models;
using System.Threading.Tasks;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Helpers  {
    /// <summary>
    /// Connection layer for the highlightDB web service.
    /// </summary>
    public static class HighlightDB  
    {  
        /// <summary>
        /// HighlightDB endpoint resolved from the appsettings.json configuration file 
        /// </summary>
        private static string highlightDBEndpoint = Startup.GetHighlightDBEndpoint();
        /// <summary>
        /// class variable to reuse the web request.
        /// </summary>
        public static HttpWebRequest WebRequest { get; set; }

        /// <summary>
        /// Sends a post request to the highlightDB web service.
        /// </summary>
        /// <param name="payload" type="string">The body to add to the payload of the post request</param>
        /// <returns>True if added correctly</returns>
        public static Boolean SendPostRequest(string payload) {
            var rtn = false;
            var result = "";
            var url = string.Concat(highlightDBEndpoint, "emailalert/");
            WebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            WebRequest.AutomaticDecompression = DecompressionMethods.GZip;
            WebRequest.Method = "post";
            if (payload.Length > 0)
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(payload);
                WebRequest.ContentLength = dataBytes.Length;
                WebRequest.ContentType = "application/json";
                using (Stream requestBody = WebRequest.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }
            }
            using (HttpWebResponse response = (HttpWebResponse)WebRequest.GetResponse())
            {
                var statusCode = response.StatusCode;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            return rtn;
        }
    }
}