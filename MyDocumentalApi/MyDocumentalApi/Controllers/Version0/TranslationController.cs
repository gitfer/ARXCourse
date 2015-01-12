using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyDocumentalTranslations;

namespace MyDocumentalApi.Controllers.Version0
{
    public class TranslationController : BaseController
    {
        public TranslationController(ILanguageTranslator languageTranslator) : base(languageTranslator)
        {
        }

        public IHttpActionResult Get(string langCode, string contentToTraslate)
        {
            return Ok(LanguageTranslator.Translate(langCode, contentToTraslate));
        }

        public IHttpActionResult Get(string langCode)
        {
            return Ok(LanguageTranslator.Translate(langCode));
        }
    }
}
