using System;
using System.Globalization;
using System.Resources;
using MyDocumentalTranslations.Exceptions;
using MyDocumentalTranslations.ExtensionMethods;
using Newtonsoft.Json;

namespace MyDocumentalTranslations.Services
{
    public class LanguageTranslatorServiceService : ILanguageTranslatorService
    {
        public string Translate(string langCode, string stringToTranslate)
        {
            var translation = GetAllTranslation(langCode, stringToTranslate);
            if (string.IsNullOrEmpty(translation))
                throw new MyDocumentalTranslationException(string.Format("Translation for {0} not found", stringToTranslate));

            return JsonConvert.SerializeObject(translation);
        }

        private string GetAllTranslation(string langCode, string stringToTranslate)
        {
            var translation = GetTranslation(langCode, stringToTranslate);
            if (string.IsNullOrEmpty(translation))
            {
                translation = GetTranslation(langCode, stringToTranslate.ToUpper());
            }
            if (string.IsNullOrEmpty(translation))
            {
                translation = GetTranslation(langCode, stringToTranslate.ToLower());
            }
            if (string.IsNullOrEmpty(translation))
            {
                translation = GetTranslation(langCode, stringToTranslate.UpperCaseFirst());
            }
            return translation;
        }

        public string Translate(string langCode)
        {
            return JsonConvert.SerializeObject(GetResourceSet(langCode));
        }

        private string GetTranslation(string langCode, string stringToTranslate)
        {
            var resourceSet = GetResourceSet(langCode);
            string translation = resourceSet.GetString(stringToTranslate);
            return translation;
        }

        private ResourceSet GetResourceSet(string langCode)
        {
            // TODO: refactoring
            var resourceName = string.Format("MyDocumentalTranslations.Resources.Language_{0}", langCode);
            ResourceManager resourceManager = new ResourceManager(resourceName, Type.GetType(resourceName).Assembly);
            ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            return resourceSet;
        }
    }
}
