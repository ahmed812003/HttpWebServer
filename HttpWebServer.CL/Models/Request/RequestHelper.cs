using HttpWebServer.CL.Common;
using System.Net;
using System.Text;

namespace HttpWebServer.CL.Models.Request
{
    public class RequestHelper
    {

        public async Task<(string , HttpStatusCode)> ProcessRequestAsync(string requestRoute, string projectOutputDirectory)
        {
            string filePath = Path.Combine(projectOutputDirectory, requestRoute);
            if (!File.Exists(filePath))
            {
                return (await HandleGlobalPages(projectOutputDirectory, CommonString.NotFoundPage) , HttpStatusCode.NotFound);
            }
            else
            {
                string fileContent = await File.ReadAllTextAsync(filePath);
                return (fileContent , HttpStatusCode.OK);
            }
        }

        public string GetRequestedResource(string request)
        {
            string[] requestLines = request.Split('\n');
            string[] requestLineParts = requestLines[0].Split(' ');
            string method = requestLineParts[0];
            string path = requestLineParts[1];
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = path.Length - 1; i >= 0; i--)
            {
                if (path[i] == '/')
                {
                    for (int j = i + 1; j < path.Length; j++)
                    {
                        stringBuilder.Append(path[j]);
                    }
                    break;
                }
            }
            return stringBuilder.ToString();
        }

        public string GetRequestedRoute(string request)
        {
            string[] requestLines = request.Split('\n');
            string[] requestLineParts = requestLines[0].Split(' ');
            string method = requestLineParts[0];
            string path = requestLineParts[1];
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 1; i < path.Length; i++)
            {
                if (path[i] == '/')
                    break;
                stringBuilder.Append(path[i]);
            }
            return stringBuilder.ToString();
        }

        public string GetContentType(string resource)
        {
            string extension = Path.GetExtension(resource).ToLower();
            return extension switch
            {
                ".html" => "text/html",
                ".js" => "application/javascript",
                ".css" => "text/css",
                ".json" => "application/json",
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".xml" => "application/xml",
                _ => "text/plain",
            };
        }

        public string[] GetAuthorizationLine(string request)
        {
            string[] requestLines = request.Split('\n');
            foreach(string line in requestLines)
            {
                string[] requestLineParts = line.Split(' ');
                if(requestLineParts[0] == "Authorization:")
                {
                    return requestLineParts;
                }
            }
            return null;
        }

        public async Task<string> HandleGlobalPages(string projectOutputDirectory , string pageName)
        {
            string filePath = Path.Combine(projectOutputDirectory, $"Static/{pageName}");
            string fileContent = await File.ReadAllTextAsync(filePath);
            return fileContent;
        }
    }
}
