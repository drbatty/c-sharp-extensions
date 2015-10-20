using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CSharpExtensions.ContainerClasses.Enums;

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

        #region counting

        public static bool SameNumberAs<TArg1, TArg2>(this IEnumerable<TArg1> enumerable, IEnumerable<TArg2> other)
        {
            return enumerable.Count() == other.Count();
        }

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

        /// <summary>
        /// Extension which returns true if and only if the IEnumerable enumerates more than one object
        /// </summary>
        /// <typeparam name="T">The type enumerated by the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to test</param>
        /// <returns>a boolean which is true if and only if the IEnumerable enumerates more than one object</returns>
        public static bool Many<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() > 1;
        }

        /// <summary>
        /// Extension which returns true if and only if the enumerable enumerates more than one object matching the query
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to test</param>
        /// <param name="query">The query to match</param>
        /// <returns>a boolean which is true if and only if the enumerable enumerates more than one object matching the query</returns>
        public static bool Many<T>(this IEnumerable<T> enumerable, Func<T, bool> query)
        {
            return query == null || enumerable.Count(query) > 1;
        }

        /// <summary>
        /// Extension which returns true if and only if the enumerable enumerates exactly one object
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to test</param>
        /// <returns>a boolean which is true if and only if the enumerable enumerates exactly one object</returns>
        public static bool OneOf<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() == 1;
        }

        /// <summary>
        /// Extension which returns true if and only if the enumerable enumerates exactly one object matching the query
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to test</param>
        /// <param name="query">The query to match</param>
        /// <returns>a boolean which is true if and only if the enumerable enumerates exactly one object matching the query</returns>
        public static bool OneOf<T>(this IEnumerable<T> enumerable, Func<T, bool> query)
        {
            return query == null || enumerable.Count(query) == 1;
        }

        /// <summary>
        /// Extension which returns true if and only if the enumerable enumerates exactly X objects
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The IEnumerable to test</param>
        /// <param name="count">The number of elements to test for</param>
        /// <returns>a boolean which is true if and only if the enumerable enumerates exactly X objects</returns>
        public static bool XOf<T>(this IEnumerable<T> enumerable, int count)
        {
            return enumerable.Count() == count;
        }

        /// <summary>
        /// Extension which returns true if and only if the enumerable enumerates exactly X objects matching the query
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to test</param>
        /// <param name="query">The query to match</param>
        /// <param name="count">The number of elements to test for</param>
        /// <returns>a boolean which is true if and only if the enumerable enumerates exactly X objects matching the query</returns>
        public static bool XOf<T>(this IEnumerable<T> enumerable, Func<T, bool> query, int count)
        {
            return query == null || enumerable.Count(query) == count;
        }

        
        /// <summary>
        /// Returns a histogram enumerable of pairs (t, n) where in each case n is the number
        /// of occurrences of t in the given enumerable enumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable whose frequencies to calculate</param>
        /// <returns>a histogram enumerable of pairs (t, n) where in each case n is the number
        /// of occurrences of t in the given enumerable enumerable</returns>
        public static IEnumerable<Tuple<T, int>> Frequencies<T>(this IEnumerable<T> enumerable)
        {
            var e = enumerable as IList<T> ?? enumerable.ToList();
            return e.Distinct().Select(t => t.Pair(e.Count(i => i.Equals(t))));
        }

        #endregion

        #region adding and removing elements

        /// <summary>
        /// Extension which appends an item to the end of an enumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to append to</param>
        /// <param name="element">The element to append</param>
        /// <returns>an enumerable, the original enumerable with the given element appended</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T element)
        {
            foreach (var t in enumerable)
                yield return t;
            yield return element;
        }

        /// <summary>
        /// Extension which prepends an item to the start of an enumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to prepend to</param>
        /// <param name="element">The element to prepend</param>
        /// <returns>an IEnumerable, the original enumerable with the given element prepended</returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, T element)
        {
            yield return element;
            foreach (var t in enumerable)
                yield return t;
        }

        /// <summary>
        /// Extension to filter out a given value from the given enumerable,
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to iterate over</param>
        /// <param name="element">The element to exclude</param>param>
        /// <returns> an enumerable which only enumerates the non-default values of the original enumerable</returns>
        public static IEnumerable<T> Exclude<T>(this IEnumerable<T> enumerable, T element)
        {
            return enumerable.Where(t => !Equals(t, element));
        }

        /// <summary>
        /// Extension to remove the last element of a given enumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable whose last element to remove</param>
        /// <returns> the given enumerable with the last element removed</returns>
        public static IEnumerable<T> RemoveLast<T>(this IEnumerable<T> enumerable)
        {
            var enumerable1 = enumerable as IList<T> ?? enumerable.ToList();
            for (var i = 0; i < enumerable1.Count() - 1; i++)
                yield return enumerable1.ElementAt(i);
        }

        /// <summary>
        /// Extension to remove the first element of a given enumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable whose first element to remove</param>
        /// <returns> the given enumerable with the first element removed</returns>
        public static IEnumerable<T> RemoveFirst<T>(this IEnumerable<T> enumerable)
        {
            var enumerable1 = enumerable as IList<T> ?? enumerable.ToList();
            for (var i = 1; i < enumerable1.Count(); i++)
                yield return enumerable1.ElementAt(i);
        }

        public static IEnumerable<T> Rotate<T>(this IEnumerable<T> enumerable, EnumerableRotationDirection direction)
        {
            var enumerable1 = enumerable as IList<T> ?? enumerable.ToList();
            switch (direction)
            {
                case EnumerableRotationDirection.Backwards:
                    for (var i = 1; i < enumerable1.Count(); i++)
                        yield return enumerable1.ElementAt(i);
                    if (enumerable1.Any())
                        yield return enumerable1.First();
                    break;

                case EnumerableRotationDirection.Forwards:
                    if (enumerable1.Any())
                        yield return enumerable1.Last();
                    for (var i = 0; i < enumerable1.Count() - 1; i++)
                        yield return enumerable1.ElementAt(i);
                    break;
            }
        }

        #endregion

        #region filtering

        /// <summary>
        /// Extension to filter out all the default values from the given enumerable,
        /// mainly used to get rid of all null values of an enumerable which enumerates a nullable type.
        /// </summary>
        /// <typeparam name="T">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to iterate over</param>
        /// <returns> an enumerable which only enumerates the non-default values of the original enumerable</returns>
        public static IEnumerable<T> NotDefault<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Exclude(default(T));
        }

        /// <summary>
        /// Extension to combine a selection with a filter
        /// </summary>
        /// <typeparam name="TSrc">The type enumerated by the enumerable</typeparam>
        /// <typeparam name="TResult">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to iterate over</param>
        /// <param name="π">Predicate to filter the enumerable</param>
        /// <param name="selector">Converter to perform the select</param>
        public static IEnumerable<TResult> WhereSelect<TSrc, TResult>(this IEnumerable<TSrc> enumerable,
            Predicate<TSrc> π, Converter<TSrc, TResult> selector)
        {
            return from t in enumerable where π(t) select selector(t);
        }

        /// <summary>
        /// Extension to conditionally apply a filter to an enumerable
        /// </summary>
        /// <typeparam name="TSource">The type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">The enumerable to iterate over</param>
        /// <param name="condition">condition to determine whether to filter the enumerable</param>
        /// <param name="π">predicate to perform the filtering</param>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> enumerable, bool condition,
            Func<TSource, bool> π)
        {
            if (π == null)
                return enumerable;
            return condition ? enumerable.Where(π) : enumerable;
        }

        /// <summary>
        /// returns a enumerable of elements enumerated by an enumerabe that do not satisfy a given predicate
        /// </summary>
        /// <typeparam name="T">the type of object enumerated by the enumerable</typeparam>
        /// <param name="enumerable">the given enumerable</param>
        /// <param name="π">the given predicate</param>
        /// <returns>an enumerable of elements enumerated by an enumerabe that do not satisfy a given predicate</returns>
        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> enumerable, Func<T, bool> π)
        {
            return enumerable.Where(t => !π(t));
        }

        /// <summary>
        /// Selects distinct elements from an enumerable with a given key selector
        /// </summary>
        /// <typeparam name="T">the type which the enumerable enumerates</typeparam>
        /// <typeparam name="TKey">the type of the key</typeparam>
        /// <param name="enumerable"></param> the enumerable to enumerate over
        /// <param name="λ">the selection function</param>
        /// <returns>the enumerable with only one representative for each value of the key selector</returns>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> λ)
        {
            return enumerable.GroupBy(λ).Select(e => e.First());
        }

        public static IEnumerable<T> Search<T>(this IEnumerable<T> enumerable, Func<T, string> searchField, string searchTerm)
        {
            return enumerable.Where(e => searchField(e).Trim().ToLower().Contains(searchTerm.Trim().ToLower()));
        }

        #endregion
    }
}