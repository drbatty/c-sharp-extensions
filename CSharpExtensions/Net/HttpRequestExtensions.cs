using System.IO;
using System.Net;

namespace CSharpExtensions.Net
{
    public static class HttpRequestExtensions
    {
        public static string GetResponseString(this HttpWebRequest request)
        {
            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                if (responseStream == null) return null;
                using (var reader = new StreamReader(responseStream))
                    return reader.ReadToEnd();
            }
        }
    }
}