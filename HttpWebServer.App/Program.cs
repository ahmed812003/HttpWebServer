using HttpWebServer.CL.Models.Configuration;
using HttpWebServer.CL.Models.Middleware;
using HttpWebServer.CL.Models.Server;
using System.Net.Sockets;

namespace HttpWebServer.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ConfigurationHelper configurationHelper = ConfigurationHelper.Instance;
            Configuration configuration = configurationHelper.LoadConfigurationFile();

            HttpServer httpServer = HttpServer.Instance;
            httpServer.Initialize(configuration.IpAddress , configuration.Port);
            httpServer.AddRoute("/static");
            httpServer.Use(new BasicAuthMiddleware());
            httpServer.Start();
            while (true)
            {
                TcpClient client = await httpServer.AcceptTcpClientAsync();
                await httpServer.HandleClientAsync(client);
            }
            //httpServer.Stop();
        }
    }
}
