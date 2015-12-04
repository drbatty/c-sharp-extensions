using System;

namespace CSharpExtensions.Text
{
    public class TextFormatter
    {
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string LowerCaseAlphabet = "abcdefghijklmnopqrstuvwxyz";

        public static string Now()
        {
            return DateTime.Now.ToS();
        }

        public static string QuestionMarks(int number)
        {
            if (number <= 0)
                return "";
            return "?" + ", ?".Repeat(number - 1);
        }

        public static string SplitterString(string splitterCandidate)
        {
            return splitterCandidate == "." ? "\\." : splitterCandidate;
        }

        public static string GetAlphabet()
        {
            return Alphabet;
        }

        public static string GetLowerCaseAlphabet()
        {
            return LowerCaseAlphabet;
        }

        public static string Placement(int x, int y)
        {
            return GetLowerCaseAlphabet().Substring(x, 1) + (y + 1).ToS();
        }

        public static int FromLowerCaseAlphabet(char a)
        {
            return a - 97;
        }

        public static int FromUpperCaseAlphabet(char a)
        {
            return a - 64;
        }

        public static int FromNumberChar(char a)
        {
            return a - 48;
        }

        public static int FromTwoDigitString(string s)
        {
            return FromNumberChar(s.CharAt(0)) * 10 + FromNumberChar(s.CharAt(1)) - 1;
        }

        /*public static String stripPackages(String className)
        {
            return className.substring(className.lastIndexOf('.') + 1);
        }*/
    }
}
