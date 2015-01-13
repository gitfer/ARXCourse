using System.Web.Http;
using MyDocumentalTranslations.Services;

namespace MyDocumentalApi.Controllers.Version0
{
    public class ValuesController : BaseController
    {
        public ValuesController(ILanguageTranslatorService languageTranslatorService) : base(languageTranslatorService)
        {
        }

        public IHttpActionResult Get()
        {
            return Ok(new[] { "Defaultaaa", "Defaultbbb" });
        }
    }
}
