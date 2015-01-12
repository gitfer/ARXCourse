using System.Web.Http;

namespace MyDocumentalApi.Controllers.Version0
{
    public class ValuesController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new[] { "Defaultaaa", "Defaultbbb" });
        }
    }
}
