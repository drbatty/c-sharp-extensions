using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Net;
using CSharpExtensions.RegularExpressions;

namespace CSharpExtensions.Text
{
    public static class StringExtensions
    {
        #region splitting and joining
        /// <summary>
        /// splits a string over a length 1 string, to save having to make a char array containing the same character as the length 1 string -
        /// throws an exception if the string to split over does not have length 1
        /// </summary>
        /// <param name="stringToSplit">the string to split</param>
        /// <param name="c">the length 1 string to split over</param>
        /// <returns>an array of string tokens representing the split of the string over the length 1 string</returns>
        public static string[] SplitOverLength1String(this string stringToSplit, string c)
        {
            if (c.Length != 1)
                throw new Exception("string must have length 1 to be converted to a char");
            return stringToSplit.Split(c.ToCharArray()[0]);
        }

        public static IEnumerable<string> Explode(this string s)
        {
            return s.Select((t, i) => s.Substring(i, 1)).ToList();
        }

        #endregion

        #region wrapping

        public static string Wrap(this string stringToWrap, string wrapper)
        {
            return wrapper + stringToWrap + wrapper;
        }

        public static string Wrap(this string stringToWrap, string leftWrapper, string rightWrapper)
        {
            return leftWrapper + stringToWrap + rightWrapper;
        }

        public static string WrapInSingleQuotes(this string s)
        {
            return s.Wrap("'");
        }

        public static string WrapInRoundBrackets(this string s)
        {
            return s.Wrap("(", ")");
        }

        public static string WrapInAngleBrackets(this string s)
        {
            return s.Wrap("<", ">");
        }

        #region xml

        public static string WrapInOpeningTag(this string s)
        {
            return s.WrapInAngleBrackets();
        }

        public static string WrapInClosingTag(this string s)
        {
            return s.Wrap("</", ">");
        }

        #endregion xml

        #endregion wrapping

        #region escaping and sanitizing

        public static string EscapeSingleQuotes(this string unescaped)
        {
            return unescaped.Replace("'", "\\\'");
        }

        #endregion

        #region parsing

        public static bool IsTwoDigitNumber(this string numberString, int max)
        {
            if (numberString.Length == 1)
                return numberString.CharAt(0).IsDigit(max);
            if (numberString.Length != 2)
                return false;
            var highDigit = max / 10;
            var lowDigit = max % 10;
            return numberString.CharAt(0).IsDigit(highDigit) &&
                   numberString.CharAt(1).IsDigit(numberString.CharAt(0) == highDigit + 48
                       ? lowDigit
                       : 10);
        }

        public static bool IsLowerCaseAlphabetic(this string s, int max)
        {
            if (s.Length != 1)
                return false;
            var c = s.ToCharArray()[0];
            return c.IsLowerCaseAlphabetic(max);
        }

        public static bool IsUpperCaseAlphabetic(this string s, int max)
        {
            if (s.Length != 1)
                return false;
            var c = s.ToCharArray()[0];
            return c.IsUpperCaseAlphabetic(max);
        }

        public static bool IsLowerCaseAlphabetic(this string s)
        {
            return s.IsLowerCaseAlphabetic(26);
        }

        public static bool IsUpperCaseAlphabetic(this string s)
        {
            return s.IsUpperCaseAlphabetic(26);
        }

        public static bool IsAlphabetic(this string s)
        {
            return s.IsLowerCaseAlphabetic() || s.IsUpperCaseAlphabetic();
        }

        // For dateString in format 17/09/1970
        public static bool RepresentsMonth(this string dateString, DateTime month)
        {
            var tokens = dateString.Split('/');

            if (tokens.Count() < 3)
                return false;

            var articleMonthString = tokens[1].TrimStart(Convert.ToChar("0"));
            var monthString = month.Month.ToS();
            return articleMonthString == monthString && tokens[2] == month.Year.ToS();
        }

        // For dateString in format 17/09/1970
        public static bool RepresentsYear(this string dateString, int year)
        {
            var tokens = dateString.Split('/');
            if (tokens.Count() < 3)
                return false;

            return tokens[2] == year.ToS();
        }

        // For dateString in format 17/09/1970
        public static int GetYear(this string dateString)
        {
            var tokens = dateString.Split('/');
            if (tokens.Count() < 3)
                return DateTime.Now.Year;

            int result;
            Int32.TryParse(tokens[2], out result);
            return result;
        }

        public static bool IsInteger(this string text)
        {
            int num;
            return Int32.TryParse(text, out num);
        }

        public static bool IsValidUrl(this string text)
        {
            return Regexs.Url.IsMatch(text);
        }

        public static bool IsDate(this string input)
        {
            if (String.IsNullOrEmpty(input)) return false;
            DateTime dt;
            return (DateTime.TryParse(input, out dt));
        }

        public static bool IsGuid(this string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");

            var format = new Regex(
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            var match = format.Match(s);

            return match.Success;
        }

        public static bool IsValidIpAddress(this string s)
        {
            return Regex.IsMatch(s, Regexs.IpAddress);
        }

        public static bool IsStrongPassword(this string s)
        {
            return Regex.IsMatch(s, @"[\d]") && Regex.IsMatch(s, @"[a-z]") &&
                Regex.IsMatch(s, @"[A-Z]") && Regex.IsMatch(s, @"[\@string~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]") && s.Length > 7;
        }

        public static bool IsDecimal(this string input)
        {
            decimal temp;

            return Decimal.TryParse(input, out temp);
        }

        public static bool IsValidEmailAddress(this string s)
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").IsMatch(s);
        }

        public static string LastToken(this string @string, char separator)
        {
            return @string.Split(separator).Last();
        }

        public static int FromTwoDigitString(this string s)
        {
            return s.CharAt(0).FromCoordinateNumberChar() * 10 + s.CharAt(1).FromCoordinateNumberChar();
        }

        #endregion parsing

        #region substrings

        public static string Tail(this string stringToTail, int lengthFromStart)
        {
            return lengthFromStart.Between(0, stringToTail.Length) ? stringToTail.Substring(lengthFromStart, stringToTail.Length - lengthFromStart) : "";
        }

        public static string RemoveTail(this string @string, int numberOfLetters)
        {
            return numberOfLetters.Between(0, @string.Length) ? @string.Substring(0, @string.Length - numberOfLetters) : "";
        }

        public static string RemoveTail(this string @string)
        {
            return @string.RemoveTail(1);
        }

        public static string Truncate(this string text, int maxLength)
        {
            const string suffix = "...";
            var truncatedString = text;

            if (maxLength <= 0) return truncatedString;
            var strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (text == null || text.Length <= maxLength) return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        public static string ReplaceFirst(this string @string, string @char, string replacement) // NEEDS TESTING
        {
            if (@char.IsNullOrEmpty())
                return @string;
            if (@char.Length > 1)
                return @string;
            var index = @string.IndexOf(@char, StringComparison.Ordinal);
            if (index == -1)
                return @string;
            return @string.Substring(0, index) + replacement +
                   @string.Substring(index + 1, @string.Length - (index + 1));
        }

        #endregion

        #region replacing

        public static string ReplaceCharacter(this string s, int index, string replacement)
        {
            return s.Substring(0, index) + replacement + s.Substring(index + 1, s.Length - index - 1);
        }

        #endregion

        #region formatting

        public static string TidyDate(this String dateString)
        {
            DateTime parsed;
            return DateTime.TryParse(dateString, out parsed) ? parsed.ToString("dd/MM/yyyy") : dateString;
        }

        /// <summary>
        /// extension to make String.Format non-static (syntactic sugar) 
        /// </summary>
        /// <param name="format">the string to format</param>
        /// <param name="args">the string replacement arguments (zero or more) </param>
        /// <returns> the formatted string </returns>
        public static string StringFormat(this string format, params object[] args)
        {
            return String.Format(format, args);
        }

        /// <summary>
        /// extension to replace the end of line characters in a string with spaces
        /// </summary>
        /// <param name="s">the string to format</param>
        /// <returns> the supplied string with end of line characters replaced with spaces </returns>
        public static string RemoveEndOfLines(this string s)
        {
            return s.Replace(Environment.NewLine, " ");
        }

        public static string RemoveDoubleQuotes(this string s)
        {
            return s.Replace(@"""", "");
        }

        public static string RemoveHtmlTags(this string s)
        {
            return new Regex(Regexs.HtmlTag).Replace(s, "");
        }

        public static string RemoveNonalphabetic(this string s)
        {
            return new Regex(Regexs.NonAlphabetic).Replace(s, "");
        }

        public static string RemoveNonalphabeticInternational(this string s)
        {
            return new Regex(Regexs.NonAlphabeticInternational).Replace(s, "");
        }

        public static string RemoveNonalphanumericInternational(this string s)
        {
            return new Regex(Regexs.NonAlphanumericInternational).Replace(s, "");
        }

        public static string RemoveExtraWhitespace(this string s)
        {
            return new Regex(Regexs.Whitespace).Replace(s.Trim(), " ");
        }

        public static string Linkify(this string text, string target = "_self")
        {
            return Regexs.DomainRegex.Replace(
                text, match =>
                {
                    var link = match.ToString();
                    var scheme = match.Groups["scheme"].Value == "https" ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

                    var url = new UriBuilder(link) {Scheme = scheme}.Uri.ToString();

                    return String.Format(@"<a href=""{0}"" target=""{1}"">{2}</a>", url, target, link);
                }
                );
        }

        public static string ConvertToPascal(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str", "Null is not a valid string!");
            if (str.Length == 0)
                return str;
            var strWords = str.Split(' ');
            strWords.Do(i =>
            {
                var strWord = strWords[i];
                var strFirstLetter = char.ToUpper(strWord[0]);
                strWords[i] = strFirstLetter + strWord.Substring(1);
            });
            return string.Join(" ", strWords);
        }

        public static string Repeat(this string s, int n)
        {
            var result = string.Empty;
            n.Times(() => result += s);
            return result;
        }

        #endregion

        #region line handling

        public static IEnumerable<string> ToLineList(this string s)
        {
            return s.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            // note: this will remove empty lines
        }

        public static string JoinLineSeparated(this IEnumerable<string> enumerable)
        {
            return String.Join(Environment.NewLine, enumerable.ToArray());
        }

        public static bool ContainsLine(this string s, string other)
        {
            return s.ToLineList().Contains(other);
        }

        public static string RemoveLine(this string s, string other)
        {
            return s.ToLineList().Where(str => str != other).JoinLineSeparated();
        }

        public static IEnumerable<string> CommaSeparatedToList(this string commaSeparated)
        {
            return commaSeparated.Split(',').Select(s => s.Trim()).ToList();
        }

        #endregion

        #region null handling

        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }

        #endregion

        #region regex handling

        public static IEnumerable<string> Matches(this string str, string pattern)
        {
            return new Regex(pattern).Matches(str).ToEnumerable();
        }

        public static bool IsMatch(this string value, string pattern)
        {
            return new Regex(pattern).IsMatch(value);
        }

        #endregion regex handling

        #region case handling

        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return String.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region looping

        public static void EachToken(this string @string, char separator, Action<string> λ)
        {
            @string.Split(separator).Each(λ);
        }

        public static string Times(this string @string, int n)
        {
            var result = string.Empty;
            n.Times(() => result += @string);
            return result;
        }

        #endregion

        #region hashing

        public static ulong ToHashInt(this string @string)
        {
            var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(@string));
            return 0.Upto(7).Inject((ulong)0, (acc, s) => acc + hash[s] << s * 8);
        }

        #endregion

        #region io

        public static void WriteToFile(this string content, string path)
        {
            using (var file = new StreamWriter(path, true))
                file.WriteLine(content);
        }

        public static void WriteToFileWithGuid(this string content, string pathTemplate)
        {
            content.WriteToFile(string.Format(pathTemplate, Guid.NewGuid()));
        }

        // content.WriteToFileWithGuid(@"F:\TestOutput\BazaarVoiceReviewsPlusStats{0}.txt");

        #endregion

        #region net

        public static string GetResponseString(this string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            return request.GetResponseString();
        }

        public static void GetContent(this string url, string localFilename, string mimeType)
        {
            var wc = new WebClient();
            wc.Headers["Accept"] = mimeType;
            wc.DownloadFile(url, localFilename);
        }

        public static void GetPdf(this string url, string localFilename)
        {
            GetContent(url, localFilename, "application/pdf");
        }

        public static void GetXml(this string url, string localFilename)
        {
            GetContent(url, localFilename, "application/xml");
        }

        #endregion

        public static List<Tuple<XmlNodeType, string>> FlattenXml(this string xml)
        {
            var elements = new List<Tuple<XmlNodeType, string>>();

            using (var reader = XmlReader.Create(new StringReader(xml)))
                while (reader.Read())
                {
                    var item1 = reader.NodeType;
                    var item2 = "";
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            item2 = reader.Name;
                            break;
                        case XmlNodeType.Text:
                            item2 = reader.Value;
                            break;
                        case XmlNodeType.XmlDeclaration:
                            item2 = reader.Name;
                            break;
                        case XmlNodeType.EndElement:
                            item2 = reader.Name;
                            break;
                    }
                    if (item1.In(XmlNodeType.Element, XmlNodeType.Text, XmlNodeType.XmlDeclaration,
                        XmlNodeType.EndElement))
                        elements.Add(new Tuple<XmlNodeType, string>(item1, item2));
                }

            return elements;
        }

        public static int NumberOfLines(this string s)
        {
            return s.Split('\n').Length;
        }

        #region culture invariance

        public static string ToS(this int o)
        {
            return o.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToS(this char c)
        {
            return c.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToS(this double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToS(this DateTime d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        #region character handling

        public static char CharAt(this string s, int index)
        {
            return s.ToCharArray()[index];
        }

        #endregion
    }
}