using System;
using System.Linq;
using System.Web.Http;
using MyDocumentalApi.Controllers.Version0;
using MyDocumentalApi.Services;
using MyDocumentalTranslations;

namespace MyDocumentalApi.Controllers.Version2
{
    public class ValuesController : BaseController
    {
        private readonly IMyValueService _myValueService;

        public ValuesController(IMyValueService myValueService, ILanguageTranslator languageTranslator)
            : base(languageTranslator)
        {
            _myValueService = myValueService;
        }

        public IHttpActionResult Get()
        {
            string[] valori = _myValueService.GetValues();
            return Ok(valori);
        }

        public IHttpActionResult Get(string param)
        {
            string translate;
            if (param == "Save")
            {
                translate = LanguageTranslator.Translate("en", "Save");
            }
            else
            {
                translate = LanguageTranslator.Translate("en", "Cancel");
            }
            
            return Ok(translate);
        }
    }
}
