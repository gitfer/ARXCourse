using System.Web.Http;
using MyDocumentalTranslations;
using MyDocumentalTranslations.Services;

namespace MyDocumentalApi.Controllers.Version0
{
    public class BaseController : ApiController
    {
        private readonly ILanguageTranslatorService _languageTranslatorService;

        public BaseController(ILanguageTranslatorService languageTranslatorService)
        {
            _languageTranslatorService = languageTranslatorService;
        }

        public ILanguageTranslatorService LanguageTranslatorService
        {
            get { return _languageTranslatorService; }
        }

    }
}