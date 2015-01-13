using System.Web.Http;
using MyDocumentalTranslations.Services;

namespace MyDocumentalApi.Controllers.Version0
{
    public class LanguageController : BaseController
    {
        public LanguageController(ILanguageTranslatorService languageTranslatorService) : base(languageTranslatorService)
        {
        }

        public IHttpActionResult Get(string languageSelected)
        {
            
        }
    }
}