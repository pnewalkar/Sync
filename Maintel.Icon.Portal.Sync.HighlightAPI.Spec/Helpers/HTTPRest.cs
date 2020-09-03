using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Spec.Helpers
{
    public static class HTTPRest {

        private static HttpWebRequest _webRequest;
        public static HttpWebRequest WebRequest
        {
            get { return _webRequest; }
            set { _webRequest = value; }
        }

        private static HttpStatusCode _statusCode;
        public static HttpStatusCode StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }

        private static string _responseString;
        public static string ResponseString
        {
            get { return _responseString; }
            set { _responseString = value; }
        }

        public static bool CheckEndpoint(string url) {
            var result = false;
            try {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) {
                    var asyncResult = socket.BeginConnect(url, 80, null, null);
                    result = asyncResult.AsyncWaitHandle.WaitOne(100, true);
                    socket.Close();
                }
            }
            catch {}
            return result;
        }

        public static string MakeHTTPRequest(string method, string url, string payload) {
            var result = "";
            WebRequest =  (HttpWebRequest) System.Net.WebRequest.Create(url);
            WebRequest.AutomaticDecompression = DecompressionMethods.GZip;
            WebRequest.Method = method;
            if(payload.Length > 0) {
                payload = payload.Replace("'", "\"");
                byte[] dataBytes = Encoding.UTF8.GetBytes(payload);
                WebRequest.ContentLength = dataBytes.Length;
                WebRequest.ContentType = "application/json";
                using(Stream requestBody = WebRequest.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }
            }
            try 
            {
                using (HttpWebResponse response = (HttpWebResponse)WebRequest.GetResponse()) {
                    _statusCode = response.StatusCode;
                    using (Stream stream = response.GetResponseStream()) {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                            ResponseString = result;
                        }
                    }
                }
            }
            // So we can capture unauthenticated, bad requests, etc.
            catch (WebException ex) 
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse) response;
                    _statusCode = httpResponse.StatusCode;
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                            ResponseString = result;
                            Console.WriteLine(ResponseString);
                        }
                    }
                }
            }

            return result;
        }
 
        public static string MakeArrayHTTPRequest(string method, string url, string payload) {
            var result = "";
            WebRequest =  (HttpWebRequest) System.Net.WebRequest.Create(url);
            WebRequest.AutomaticDecompression = DecompressionMethods.GZip;
            WebRequest.Method = method;
            if(payload.Length > 0) {
                payload = payload.Replace("'", "\"");
                byte[] dataBytes = Encoding.UTF8.GetBytes(payload);
                WebRequest.ContentLength = dataBytes.Length;
                WebRequest.ContentType = "application/json";
                using(Stream requestBody = WebRequest.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }
            }
            using (HttpWebResponse response = (HttpWebResponse)WebRequest.GetResponse()) {
                _statusCode = response.StatusCode;
                using (Stream stream = response.GetResponseStream()) {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                        ResponseString = result;
                    }
                }
            }
            return result;
        }
       
    
    }
}