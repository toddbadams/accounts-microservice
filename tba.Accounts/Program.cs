using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace tba.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:8123/";
            var config = new HttpSelfHostConfiguration(baseAddress);

            config.Routes.MapHttpRoute(
                                name: "DefaultApi",
                                routeTemplate: "api/{controller}/{id}",
                                defaults: new { id = RouteParameter.Optional });

            Console.WriteLine("Instantiating The Server...");
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Server is Running Now... @ " + baseAddress);
                Console.ReadLine();
            }
        }
    }
}
