namespace MyDocumentalTranslations.ExtensionMethods
{
    public static class StringExtensions
    {

        public static string UpperCaseFirst(this string elem)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(elem))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(elem[0]) + elem.Substring(1);
        }
    }
}