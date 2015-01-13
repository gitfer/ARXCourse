using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MyDocumentalApi.Filters
{
    public class GenericErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ReasonPhrase = "error",
                Content = new StringContent(actionExecutedContext.Exception.Message)
            };
            actionExecutedContext.Response = response;

        }
    }
}