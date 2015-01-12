using System.Web.Http;
using MyDocumentalTranslations;

namespace MyDocumentalApi.Controllers.Version0
{
    public class BaseController : ApiController
    {
        private readonly ILanguageTranslator _languageTranslator;

        public BaseController(ILanguageTranslator languageTranslator)
        {
            _languageTranslator = languageTranslator;
        }

        public ILanguageTranslator LanguageTranslator
        {
            get { return _languageTranslator; }
        }

    }
}