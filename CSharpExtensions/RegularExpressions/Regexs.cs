using System.Text.RegularExpressions;

namespace CSharpExtensions.RegularExpressions
{
    public class Regexs
    {
        public static string IpAddress = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
        public static string Domain =
            @"(((?<scheme>http(s)?):\/\/)?([\w-]+?\.\w+)+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\,]*)?)";
        public static string Url = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        public static string Hyperlink = @"<a[^>]+href=""([^""]+)""[^>]*>";
        public static string DoubleQuotes = @"""(.+?)""";
        public static string HtmlTag = @"<[^>]*>";
        public static string NonAlphabetic = @"[^A-Za-z -]+";
        public static string NonAlphabeticInternational = @"[^éA-Za-z -]+";
        public static string NonAlphanumericInternational = @"[^-'ÇçÀàÂâÈèÉéÊêÄäÜüÖößåÅÆæØøĄąĘęÓóĆćŁłŃńŚśŹźŻżбвгдёжзийклмнптуфцчшщъыьэюяБГДЁЖЗИЙКЛПУФЦЧШЩЪЫЬЭЮЯ()A-Za-z0-9 ()]+";
        public static string Whitespace = @"[ \t\r\n]+";
        public static string Integer = @"-?[0-9]+$";
        public static string Decade = @"-?[0-9]+$s";
        public static readonly Regex DomainRegex = new Regex(Domain, RegexOptions.Compiled | RegexOptions.Multiline);
    }
}