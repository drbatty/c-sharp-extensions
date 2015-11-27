namespace CSharpExtensions.Text
{
    public static class CharExtensions
    {
        #region parsing

        public static bool IsDigit(this char c, int max)
        {
            if (max < 0)
                return false;
            if (max > 9)
                max = 9;
            return c > 47 && c <= 48 + max;
        }

        public static bool IsDigit(this char c)
        {
            return c.IsDigit(9);
        }

        public static bool IsLowerCaseAlphabetic(this char c, int max)
        {
            if (max < 0)
                return false;
            if (max > 26)
                max = 26;
            return c > 96 && c < 97 + max;
        }

        public static bool IsUpperCaseAlphabetic(this char c, int max)
        {
            if (max < 0)
                return false;
            if (max > 26)
                max = 26;
            return c > 64 && c < 65 + max;
        }

        public static bool IsLowerCaseAlphabetic(this char c)
        {
            return IsLowerCaseAlphabetic(c, 26);
        }

        public static bool IsUpperCaseAlphabetic(this char c)
        {
            return IsUpperCaseAlphabetic(c, 26);
        }

        public static bool IsAlphabetic(this char c)
        {
            return c.IsLowerCaseAlphabetic() || c.IsUpperCaseAlphabetic();
        }

        public static int FromCoordinateLowerCaseLetterChar(this char a)
        {
            return a - 97;
        }

        public static int FromCoordinateNumberChar(this char a)
        {
            return a - 48;
        }
        #endregion
    }
}
