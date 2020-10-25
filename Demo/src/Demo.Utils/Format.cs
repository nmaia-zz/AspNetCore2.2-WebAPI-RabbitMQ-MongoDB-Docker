using System.Globalization;

namespace Demo.Utils
{
    public class Format
    {
        public static string StringToTitleCase(string str)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(str);
        }
    }
}
