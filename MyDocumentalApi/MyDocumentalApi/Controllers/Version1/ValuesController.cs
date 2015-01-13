using System.Web.Http;
using MyDocumentalTranslations.Services;

namespace MyDocumentalApi.Controllers.Version1
{
    public class ValuesController : Version0.ValuesController
    {
        public ValuesController(ILanguageTranslatorService languageTranslatorService) : base(languageTranslatorService)
        {
        }
    }
}
