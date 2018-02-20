using System.IO;
using System.Net;

namespace BusinessLogicLayer.RequestProvider
{
    public class HttpRequestProvider
    {
        public string Get(string uri, string contentType)
        {
            var request = WebRequest.Create(uri);
            request.ContentType = contentType;
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return responseFromServer;
        }
    }
}