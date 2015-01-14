using System.Collections.Generic;

namespace MyDocumentalTranslations.Services
{
    public interface ILanguageTranslatorService
    {
        string Translate(string langCode, string stringToTranslate);
        string Translate(string langCode);
    }
}