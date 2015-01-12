using System.Web.Http;

namespace MyDocumentalApi.Controllers.Version2
{
    public class ValuesController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new[] {"V2aaa", "V2bbb"});
        }
    }
}
