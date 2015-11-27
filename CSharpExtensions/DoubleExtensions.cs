using System;

namespace CSharpExtensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// Extension that displays a given double as a sum of money, always with two decimal places
        /// </summary>
        /// <param name="d"> The double to display as a sum of money</param>
        /// <returns> a string which displays the given double as a sum of money, with two decimal places</returns>
        public static string DisplayAsSumOfMoney(this double d)
        {
            return string.Format("{0:N2}", Math.Truncate(d * 100) / 100);
        }

        public static double Distance(this double d, double other)
        {
            return Math.Abs(d - other);
        }
    }
}