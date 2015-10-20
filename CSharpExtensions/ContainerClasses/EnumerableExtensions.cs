using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpExtensions.ContainerClasses
{
    public static class EnumerableExtensions
    {
        #region comprehension

        /// <summary>
        /// Extension to apply an action to each item enumerated by the given IEnumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to iterate over</param>
        /// <param name="λ">The action to apply to each item enumerated by the given IEnumerable</param>
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> λ)
        {
            foreach (var t in enumerable)
                λ(t);
        }

        /// <summary>
        /// Inject takes an accumulator value, and iterates through an enumerable, accumulating each value using the lambda
        /// Similar to Linq Aggregate, but with an independent accumulator, like Ruby's inject method
        /// </summary>
        /// <typeparam name="TAccumulator">The type enumerated by the IEnumerable</typeparam>
        /// <typeparam name="T">The type enumerated by the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to iterate over</param>
        /// <param name="accumulatorStartValue">The start value for the accumulator</param>
        /// <param name="λ">delegate to transform the accumulator</param>
        public static TAccumulator Inject<TAccumulator, T>(this IEnumerable<T> enumerable,
            TAccumulator accumulatorStartValue,
            Func<TAccumulator, T, TAccumulator> λ)
        {
            if (λ == null)
                return accumulatorStartValue;
            var accumulator = accumulatorStartValue;
            enumerable.Each(t => accumulator = λ(accumulator, t));
            return accumulator;
        }

        /// <summary>
        /// Similar to the LINQ extension Sum, this extension forms a product of a function of items coming from an enumerable.
        /// Usage mainly mathematical.
        /// </summary>
        /// <typeparam name="T">The type enumerated by the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to iterate over</param>
        /// <param name="λ">lambda which maps the iEnumerable term to the product term</param>
        /// <returns></returns>
        public static int Product<T>(this IEnumerable<T> enumerable, Func<T, int> λ)
        {
            return enumerable.Inject(1, (x, y) => x * λ(y));
        }

        /// <summary>
        /// Forms the product of all items enumerated by an enumerable of integers
        /// </summary>
        /// <param name="enumerable">An enumerable of integers to multiply</param>
        /// <returns></returns>
        public static int Product(this IEnumerable<int> enumerable)
        {
            return enumerable.Product(i => i);
        }

        #endregion

        #region looping

        public static void Do<T>(this IEnumerable<T> enumerable, Action<int> λ)
        {
            enumerable.Count().Do(λ);
        }

        #endregion
    }
}
