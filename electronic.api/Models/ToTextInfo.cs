using System.Globalization;

namespace electronic.api.Models
{
    public static class ToTextInfo
    {
        public static string CustomToTitleCase(this string text)
        {
            TextInfo myTI = new CultureInfo("tr-TR", false).TextInfo;
            return myTI.ToTitleCase(text != null ? text : "");
        }

        public static string CustomToLower(this string text)
        {
            TextInfo myTI = new CultureInfo("tr-TR", false).TextInfo;
            return myTI.ToLower(text != null ? text : "");
        }

        public static string CustomToUpper(this string text)
        {
            TextInfo myTI = new CultureInfo("tr-TR", false).TextInfo;
            return myTI.ToUpper(text != null ? text : "");
        }

    }
}
