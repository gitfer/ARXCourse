using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
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
            var resourceSet = GetResourceSet(langCode);
            IDictionary<string, string>  dic =  resourceSet.Cast<DictionaryEntry>()
                .ToDictionary(x => x.Key.ToString(),
                    x => x.Value.ToString());
            var entries = dic.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));

            return "{" + string.Join(",", entries) + "}";
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

    public class Risorsa
    {
        public String Key { get; set; }
        public String Value { get; set; }
    }
}
