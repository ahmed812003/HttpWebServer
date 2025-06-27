using System.Net;
using System.Text;

namespace HttpWebServer.CL.Models.Response
{
    public class ResponseHelper
    {
        private readonly string _httpResponse;

        public ResponseHelper()
        {
            _httpResponse = "HTTP/1.1 {0}\r\n" +
                            "Content-Type:{1}\r\n" +
                            "Content-Length:{2}\r\n" +
                            "\r\n {3}";
        }

        public byte[] ProcessResponseAsync(string contentType, string responseBody, HttpStatusCode httpStatusCode)
        {
            string httpResponse =  string.Format(_httpResponse, GetResponsesStatus(httpStatusCode), contentType, responseBody.Length, responseBody);
            Console.WriteLine("Response:\n" + httpResponse);
            byte[] responseData = Encoding.UTF8.GetBytes(httpResponse);
            return responseData;
        }

        public string GetResponsesStatus(HttpStatusCode httpStatusCode)
        {
            return httpStatusCode switch
            {
                HttpStatusCode.OK => "200 OK",
                HttpStatusCode.NotFound => "404 Not Found",
                HttpStatusCode.Unauthorized => "401 Unauthorized",
                HttpStatusCode.InternalServerError => "500 Internal Server Error",
                _ => ""
            };
        }
    }
}
