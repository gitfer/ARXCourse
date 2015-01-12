using System;
using System.Linq;
using System.Web.Http;
using MyDocumentalApi.Services;

namespace MyDocumentalApi.Controllers.Version2
{
    public class ValuesController : ApiController
    {
        private readonly IMyValueService _myValueService;

        public ValuesController(IMyValueService myValueService)
        {
            _myValueService = myValueService;
        }

        public IHttpActionResult Get()
        {
            string[] valori = _myValueService.GetValues();
            return Ok(valori);
        }
    }
}
