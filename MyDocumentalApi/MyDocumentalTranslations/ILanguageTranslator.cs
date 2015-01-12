namespace MyDocumentalTranslations
{
    public interface ILanguageTranslator
    {
        string Translate(string langCode, string stringToTranslate);
        string Translate(string langCode);
    }
}