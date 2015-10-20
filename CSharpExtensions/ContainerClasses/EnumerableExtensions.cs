using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// Applies an action to each pair (a, b) where a and b are both enumerated by a given enumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to iterate over</param>
        /// <param name="λ">The action to apply to each pair</param>
        public static void EachPair<T>(this IEnumerable<T> enumerable, Action<T, T> λ)
        {
            var enumerable1 = enumerable as IList<T> ?? enumerable.ToList();
            enumerable1.Each(t => enumerable1.Each(u => λ(t, u)));
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

        #region looping

        public static void Do<T>(this IEnumerable<T> enumerable, Action<int> λ)
        {
            enumerable.Count().Do(λ);
        }

        #endregion

        #endregion

        #region index handling

        public static bool AllIndices<T>(this IEnumerable<T> enumerable, Func<int, bool> λ)
        {
            return 0.Upto(enumerable.Count() - 1).All(λ);
        }

        public static void EachIndex<T>(this IEnumerable<T> enumerable, Action<int> λ)
        {
            0.Upto(enumerable.Count() - 1).Each(λ);
        }

        private static int Index<T>(this IEnumerable<T> enumerable, Func<T, bool> λ,
            Func<IEnumerable<int>, Func<int, bool>, int> μ)
        {
            var list = enumerable as IList<T> ?? enumerable.ToList();
            if (0.Upto(list.Count() - 1).None(i => λ(list[i])))
                return -1;
            return μ(0.Upto(list.Count() - 1), i => λ(list[i]));
        }

        public static int FirstIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> λ)
        {
            return enumerable.Index(λ, (e, f) => e.First(f));
        }

        public static int LastIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> λ)
        {
            return enumerable.Index(λ, (e, f) => e.Last(f));
        }

        public static int CountIndices<T>(this IEnumerable<T> enumerable, Func<int, bool> func)
        {
            return 0.Upto(enumerable.Count() - 1).Count(func);
        }

        #endregion

        #region counting

        /// <summary>
        /// Extension equivalent to !Any but more readable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to test</param>
        /// <returns>a boolean which is true if and only if the IEnumerable enumerates no objects</returns>
        public static bool None<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        /// <summary>
        /// Extension equivalent to !Any (with a query) but more readable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to test</param>
        /// <param name="query">The query to match</param>
        /// <returns>a boolean which is true if and only if the enumerable enumerates no objects matching the query</returns>
        public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> query)
        {
            return query == null || !enumerable.Any(query);
        }

        #endregion

        #region conversion

        /// <summary>
        /// Extension to convert an IEnumerable to a enumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the IEnumerable/ contained in the enumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to convert</param>
        /// <returns>A enumerable containing the same items as the IEnumerable enumerates</returns>
        public static Collection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new Collection<T>();
            enumerable.Each(collection.Add);
            return collection;
        }

        #endregion


    }
}
