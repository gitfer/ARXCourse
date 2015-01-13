using System;
using System.Linq;
using System.Web.Http;
using MyDocumentalApi.Controllers.Version0;
using MyDocumentalApi.Services;
using MyDocumentalTranslations;
using MyDocumentalTranslations.Services;

namespace MyDocumentalApi.Controllers.Version2
{
    public class ValuesController : Version0.ValuesController
    {
        private readonly IMyValueService _myValueService;

        public ValuesController(IMyValueService myValueService, ILanguageTranslatorService languageTranslatorService)
            : base(languageTranslatorService)
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
