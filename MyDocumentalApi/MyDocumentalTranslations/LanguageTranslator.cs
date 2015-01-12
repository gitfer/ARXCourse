using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using MyDocumentalTranslations.Resources;
using Newtonsoft.Json;

namespace MyDocumentalTranslations
{
    public class LanguageTranslator : ILanguageTranslator
    {
        public string Translate(string langCode, string stringToTranslate)
        {
            var resourceSet = GetResourceSet(langCode);

            return JsonConvert.SerializeObject(resourceSet.GetString(stringToTranslate));
        }

        public string Translate(string langCode)
        {
            var resourceSet = GetResourceSet(langCode);
            IEnumerable<DictionaryEntry> dictionaryEntries = resourceSet.Cast<DictionaryEntry>();
            dictionaryEntries.ToDictionary(x => x.Key.ToString(),
                x => x.Value.ToString());
            
            return JsonConvert.SerializeObject(dictionaryEntries);
        }

        private ResourceSet GetResourceSet(string langCode)
        {
            Assembly assembly = this.GetType().Assembly;
            // TODO: refactoring
            ResourceManager resourceManager = new ResourceManager("MyDocumentalTranslations.Resources.Language_"+langCode, Type.GetType("MyDocumentalTranslations.Resources.Language_"+langCode).Assembly);

            string stringa = resourceManager.GetString("Save");
            
            ResourceSet resourceSet = resourceManager.GetResourceSet(new CultureInfo("en"), false, false);
            return resourceSet;
        }
    }
}
