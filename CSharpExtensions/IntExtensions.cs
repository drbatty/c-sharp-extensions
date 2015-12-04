using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Delegate;

namespace CSharpExtensions
{
    public static class IntExtensions
    {
        #region enumerable creation

        /// <summary>
        /// Extension which allows for writing the more readable 1.Upto(10) instead of Enumerable.Range(1,10)</summary>
        /// <param name="start">the first integer in the range</param>
        /// <param name="end">the last integer in the range (inclusively)</param>
        /// <returns> an enumerable containing all of the integers starting at start and ending at end, in order </returns>
        public static IEnumerable<int> Upto(this int start, int end) // like Ruby method
        {
            return end < start ? new List<int>() : Enumerable.Range(start, end - start + 1);
        }

        public static IEnumerable<int> Upto(this int start, int end, Func<int, int> λ)
        {
            return λ == null ? new List<int>() : start.Upto(end).Select(λ);
        }

        public static int[] ArrayUpto(this int start, int end)
        {
            return start.Upto(end).ToArray();
        }

        #endregion

        #region looping

        public static void DoUpto(this int start, int end, Action<int> λ)
        {
            if (λ != null)
                start.Upto(end).Each(λ);
        }

        public static void DoZeroUpto(this int end, Action<int> λ)
        {
            0.DoUpto(end, λ);
        }

        public static void Do(this int end, Action<int> λ)
        {
            DoZeroUpto(end - 1, λ);
        }

        public static void Do(this int end, int innerEnd, Action<int, int> λ)
        {
            end.Do(i => innerEnd.Do(j => λ(i, j)));
        }

        public static void Do(this int end, Action<int, int> λ)
        {
            end.Do(end, λ);
        }

        public static void DoTriangular(this int end, Action<int, int> λ)
        {
            end.Do(i => (i + 1).Upto(end).Do(j => λ(i, j)));
        }

        public static bool Any(this int start, int end, Func<int, bool> π)
        {
            return start.Upto(end - 1).Any(π);
        }

        public static bool Any(this int end, Func<int, bool> π)
        {
            return 0.Any(end, π);
        }

        public static bool All(this int start, int end, Func<int, bool> π)
        {
            return start.Upto(end - 1).All(π);
        }

        public static bool All(this int end, Func<int, bool> π)
        {
            return 0.All(end, π);
        }

        /// <summary>
        /// 	Performs the specified action n times based on the underlying int value.
        /// </summary>
        /// <param name = "value">The value.</param>
        /// <param name = "λ">The action.</param>
        public static void Times(this int value, Action λ)
        {
            if (λ != null)
                (1.Upto(value)).Each(i => λ());
        }

        #endregion

        #region probabilistic

        public static void MaybeDo(this int end, double probability, Action<int, int> λ)
        {
            end.MaybeDo(end, probability, λ);
        }

        public static void MaybeDo(this int end, int innerEnd, double probability, Action<int, int> λ)
        {
            end.Do(i => innerEnd.Do(j => λ.ExecuteWithProbability(i, j, probability)));
        }

        #endregion

        #region mathematical

        public static int Distance(this int i, int j)
        {
            return Math.Abs(i - j);
        }

        #endregion

        public static TimeSpan Seconds(this int seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        public static TimeSpan Minutes(this int minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        //1.Seconds()
        //20.Minutes()

        public static string ToPluralString(this int value)
        {
            return value == 1 ? string.Empty : "s";
        }

        public static int DecrementIfNonZero(this int value)
        {
            return value == 0 ? value : value - 1;
        }
    }
}