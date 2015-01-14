using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using MyDocumentalTranslations.Services;

namespace MyDocumentalApi.Controllers.Version0
{
    public class LanguagesController : BaseController
    {
        const string Origin = "Origin";
        const string AccessControlRequestMethod = "Access-Control-Request-Method";
        const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";

        public LanguagesController(ILanguageTranslatorService languageTranslatorService) : base(languageTranslatorService)
        {
        }

        //public object Get(string lang)
        //{
        //    string traduzione = LanguageTranslatorService.Translate(lang);

        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //    var stream = new MemoryStream(Encoding.UTF8.GetBytes(traduzione));
        //    result.Content = new StreamContent(stream);
        //    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    {
        //        FileName = "Language_" + lang + ".json"
        //    };
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    result.Content.Headers.ContentLength = stream.Length;
        //    result.Headers.Add(AccessControlAllowCredentials, "*");
        //    result.Content.Headers.Add(AccessControlAllowCredentials, "*");

        //    return result;
        //}


        public async Task<object> Get(string lang)
        {
            string traduzione = LanguageTranslatorService.Translate(lang);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(traduzione));
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Language_" + lang + ".json"
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            result.Content.Headers.ContentLength = stream.Length;
            result.Headers.Add(AccessControlAllowCredentials, "*");
            result.Content.Headers.Add(AccessControlAllowCredentials, "*");

            return result;
        }
    }
}