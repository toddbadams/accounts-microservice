using System.Web.Http;

namespace tba.SelfHost
{
    public class HelloWorldController : ApiController
    {
        public string Get()
        {
            return "Hi!, Self-Hosted Web Api Application Get()";
        }

        public string Get(int id)
        {
            return "Hi!, Self-Hosted Web Api Application Get() With Id: " + id;
        }
    }
}