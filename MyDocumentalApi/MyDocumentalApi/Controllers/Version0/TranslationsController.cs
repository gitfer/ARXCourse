using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MyDocumentalTranslations;
using MyDocumentalTranslations.Exceptions;
using MyDocumentalTranslations.Services;

namespace MyDocumentalApi.Controllers.Version0
{
	public class TranslationsController : BaseController
	{
		public TranslationsController(ILanguageTranslatorService languageTranslatorService) : base(languageTranslatorService)
		{
		}

		[HttpGet]
		public IHttpActionResult Get(string[] parametri)
		{
		    string translate;
		    try
		    {
                translate = LanguageTranslatorService.Translate(parametri[0], parametri[1]);
		    }
		    catch (MyDocumentalTranslationException)
		    {
		        return NotFound();
		    }
			return Ok(translate);
		}

		public IHttpActionResult Get(string langCode)
		{
			return Ok(LanguageTranslatorService.Translate(langCode));
		}
	}
}
