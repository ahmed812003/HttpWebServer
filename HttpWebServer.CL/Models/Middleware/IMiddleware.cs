using HttpWebServer.CL.Models.Request;
using HttpWebServer.CL.Models.Response;
using System.Net;

namespace HttpWebServer.CL.Models.Middleware
{
    public interface IMiddleware
    {
        Task<HttpStatusCode> Handle(string request, RequestHelper requestHelper, ResponseHelper responseHelper);
    }
}
