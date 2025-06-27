using HttpWebServer.CL.Models.Request;
using HttpWebServer.CL.Models.Response;
using System.Net;

namespace HttpWebServer.CL.Models.Middleware
{
    public class BasicAuthMiddleware : IMiddleware
    {

        public async Task<HttpStatusCode> Handle(string request , RequestHelper requestHelper , ResponseHelper responseHelper)
        {
            string[] authHeader = requestHelper.GetAuthorizationLine(request);
            if (authHeader is null)
            {
                return await Task.FromResult(HttpStatusCode.Unauthorized);
            }
            return await Task.FromResult(HttpStatusCode.Accepted);
        }
    }
}
