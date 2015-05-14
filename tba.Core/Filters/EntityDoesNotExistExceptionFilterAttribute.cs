using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using tba.Core.Exceptions;

namespace tba.Core.Filters
{
    public class EntityDoesNotExistExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is EntityDoesNotExistException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }
    }
}
