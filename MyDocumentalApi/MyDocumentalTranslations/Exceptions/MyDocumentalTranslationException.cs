using System;

namespace MyDocumentalTranslations.Exceptions
{
    public class MyDocumentalTranslationException : Exception
    {
        public MyDocumentalTranslationException(string message) : base(message)
        {
        }
    }
}