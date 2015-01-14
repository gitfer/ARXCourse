using System.Web.Http;
using MyDocumentalApi.Controllers.Version0;
using MyDocumentalApi.Services;
using MyDocumentalTranslations.Services;

namespace MyDocumentalApi.Controllers.Version2_1
{
    public class ValuesController : BaseController
    {
        private readonly IMyValueService _myValueService;

        public ValuesController(IMyValueService myValueService, ILanguageTranslatorService languageTranslatorService)
            : base(languageTranslatorService)
        {
            _myValueService = myValueService;
        }

        public IHttpActionResult Get()
        {

            string[] valori = new string[] { "aaaV2.1", "bbbV2.1" };
            return Ok(valori);
        }

    }
}
