using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CSharpExtensions.ContainerClasses.Enums;
using CSharpExtensions.Mathematical.Sets;

namespace CSharpExtensions.ContainerClasses
{
    public static class EnumerableExtensions
    {
        private static readonly Random Random = new Random();

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

        /// <summary>
        /// converts an IEnumerable to a set
        /// </summary>
        /// <typeparam name="T">the type enumerated by the IEnumerable</typeparam>
        /// <param name="enumerable">the IEnumerable to convert</param>
        /// <returns>a set containing the same elements as the IEnumerable enumerates</returns>
        public static Set<T> ToSet<T>(this IEnumerable<T> enumerable)
        {
            return new Set<T>(enumerable);
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
        /// Returns a distinct enumerable of items from the source enumerable using the predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="λ"></param>
        /// <returns></returns>
        /// http://stackoverflow.com/questions/520030/why-is-there-no-linq-method-to-return-distinct-values-by-a-predicate
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> λ)
        {
            return source.DistinctBy(λ, EqualityComparer<TKey>.Default);
        }
        //can be used to produce transversals for equivalence relations, e.g. a set of coset representatives for a subgroup of a group

        private static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> λ, IEqualityComparer<TKey> comparer)
        {
            if (λ == null)
                throw new ArgumentNullException("λ");
            return DistinctByImpl(source, λ, comparer);
        }

        private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> λ, IEqualityComparer<TKey> comparer)
        {
            var knownKeys = new HashSet<TKey>(comparer);
            return source.Where(element => knownKeys.Add(λ(element)));
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

        #region randomization

        /// <summary>
        /// Extension to randomly shuffle the elements of an IEnumerable
        /// </summary>
        /// <typeparam name="T">The type enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">The IEnumerable to shuffle</param>
        /// <returns>The shuffled version of the IEnumerable</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> iEnumerable)
        {
            return iEnumerable.OrderBy(t => Random.Next());
        }

        /// <summary>
        /// Take random items
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="this">Collection</param>
        /// <param name="count">Number of items to take</param>
        /// <returns>New enumerable</returns>
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> @this, int count)
        {
            return @this.Shuffle().Take(count);
        }

        /// <summary>
        /// Take random item
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="this">Collection</param>
        /// <returns>Item</returns>
        public static T TakeRandom<T>(this IEnumerable<T> @this)
        {
            return @this.TakeRandom(1).Single();
        }

        #endregion

        #region mathematical

        /// <summary>
        /// Computes all the combinations of elements from a given IEnumerable
        /// </summary>
        /// <typeparam name="T">the type which the IEnumerable enumerates</typeparam>
        /// <param name="source">the IEnumerable which forms the enumerable of the combinations</param>
        /// <param name="select">the number of elements to select</param>
        /// <param name="repetition">whether or not repetition is allowed</param>
        /// <returns>an IEnumerable of enumerables, which enumerates the combinations of the given IEnumerable</returns>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int select,
            bool repetition = false)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            return select == 0
                ? new[] { new T[0] }
                : enumerable.SelectMany((element, index) => enumerable
                    .Skip(repetition ? index : index + 1)
                    .Combinations(select - 1, repetition)
                    .Select(c => new[] { element }.Concat(c)));
        }

        /// <summary>
        /// returns the value in an IEnumerable which minimizes a double-valued function of the IEnumerable
        /// </summary>
        /// <typeparam name="T">the type of object enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">the IEnumerable to minimize over</param>
        /// <param name="λ">the function to minimize</param>
        /// <returns>the value in an IEnumerable which minimizes the given double-valued function of the IEnumerable</returns>
        public static Tuple<T, double> Minimizer<T>(this IEnumerable<T> iEnumerable, Func<T, double> λ)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            if (enumerable.None() || λ == null)
                return null;
            var min = enumerable.Min(λ);
            return new Tuple<T, double>(enumerable.FirstOrDefault(v => λ(v) == min), min);
        }

        /// <summary>
        /// returns the value in an IEnumerable which maximizes a double-valued function of the IEnumerable
        /// </summary>
        /// <typeparam name="T">the type of object enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">the IEnumerable to maximize over</param>
        /// <param name="λ">the function to maximize</param>
        /// <returns>the value in an IEnumerable which maximizes the given double-valued function of the IEnumerable</returns>
        public static Tuple<T, double> Maximizer<T>(this IEnumerable<T> iEnumerable, Func<T, double> λ)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            if (enumerable.None() || λ == null)
                return null;
            var max = enumerable.Max(λ);
            return new Tuple<T, double>(enumerable.FirstOrDefault(v => λ(v) == max), max);
        }

        /// <summary>
        /// the minimum over a pair of enumerables
        /// </summary>
        /// <typeparam name="T">the type of object enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">the operand IEnumerable</param>
        /// <param name="other">the enumerable2 given IEnumerable</param>
        /// <param name="λ">the function to minimize</param>
        /// <returns>the minimum over the given pair of enumerables</returns>
        public static double Min<T>(this IEnumerable<T> iEnumerable, IEnumerable<T> other,
            Func<T, T, double> λ)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            var ss = other as IList<T> ?? other.ToList();
            if (enumerable.None() || ss.None() || λ == null)
                return default(double);
            return enumerable.Min(s1 => ss.Min(s2 => λ(s1, s2)));
        }

        /// <summary>
        /// returns the pair of values which minimize a function on a pair of enumerables, and the minimum value
        /// </summary>
        /// <typeparam name="T">the type enumerated by the IEnumerable</typeparam>
        /// <param name="iEnumerable">the operand IEnumerable</param>
        /// <param name="other">the enumerable2 IEnumerable</param>
        /// <param name="lambda">the function to minimize</param>
        /// <returns>the pair of values which minimize a function on a pair of enumerables, and the minimum value</returns>
        public static Tuple<Tuple<T, T>, double> Minimizers<T>(this IEnumerable<T> iEnumerable, IEnumerable<T> other,
            Func<T, T, double> λ)
        {
            var enumerable = iEnumerable as IList<T> ?? iEnumerable.ToList();
            var ss = other as IList<T> ?? other.ToList();
            if (enumerable.None() || ss.None() || λ == null)
                return null;
            var minimizers = enumerable.Select(t1 => t1.Pair(ss.Minimizer(t2 => λ(t1, t2)).Item1));
            var minimizer = minimizers.Minimizer(tuple => λ(tuple.Item1, tuple.Item2));
            return minimizer.Item1.Pair(minimizer.Item2);
        }

        #endregion

        #region null handling

        /// <summary>
        /// returns an empty enumerable if the given enumerable is null
        /// </summary>
        /// <typeparam name="T">the type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">the given enumerable</param>
        /// <returns>either the given IEnumerable or an empty enumerable if the given enumerable is null</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        #endregion

        #region string formatting

        #region comma separation

        /// <summary>
        /// returns a string representation of a given enumerable of strings with the elements separated by commas and no spacing
        /// </summary>
        /// <typeparam name="T">the type enumerated by the enumerable</typeparam>
        /// <param name="stringList">the li</param>
        /// <returns>a string representation of a given enumerable of strings with the elements separated by commas and no spacing</returns>
        public static string CommaSeparate<T>(this IEnumerable<T> stringList)
        {
            return stringList != null ? string.Join(",", stringList) : string.Empty;
        }

        public static string SpacedCommaSeparate<T>(this IEnumerable<T> stringList)
        {
            return stringList != null ? string.Join(" , ", stringList) : string.Empty;
        }

        public static string SpacedAfterCommaSeparate<T>(this IEnumerable<T> stringList)
        {
            return stringList != null ? string.Join(", ", stringList) : string.Empty;
        }

        public static string CommaSeparate<T>(this IEnumerable<T> stringList, string prefix)
        {
            if (stringList == null)
                return string.Empty;
            var enumerable = stringList as IList<T> ?? stringList.ToList();
            var result = enumerable.Any() ? prefix : string.Empty;
            return result + string.Join("," + prefix, enumerable);
        }

        /// <summary>
        /// takes a generic enumerable and generates an enumerable of all the string representations
        /// of the given enumerable
        /// </summary>
        /// <typeparam name="T">the type enumerated by the enumerable</typeparam>
        /// <param name="enumerable">the given enumerable</param>
        /// <returns>an enumerable of all the string representations of the given enumerable</returns>
        public static IEnumerable<string> ToStrings<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select(t => t.ToString());
        }

        /// <summary>
        /// returns a string representation of a list of strings in the form
        /// "a, b and c", i.e. with commas and "and"
        /// </summary>
        /// <param name="enumerable">enumerable</param>
        /// <returns>the enumerable whose string representation to form</returns>
        public static string EnglishSeparate(this IEnumerable<string> enumerable)
        {
            var enumerable1 = enumerable as IList<string> ?? enumerable.ToList();
            var result = string.Join(", ", enumerable1.RemoveLast().ToArray());
            if (enumerable1.Count > 1)
                result += " and ";
            result += enumerable1.Last();
            return result;
        }

        #endregion

        public static string ToString<T>(this IEnumerable<T> enumerable, string separator)
        {
            return ToString(enumerable, t => t.ToString(), separator);
        }

        public static string ToString<T>(this IEnumerable<T> enumerable, Func<T, string> λ, string separator)
        {
            var sb = new StringBuilder();
            enumerable.Each(item =>
            {
                sb.Append(λ(item));
                sb.Append(separator);
            });
            return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));
        }

        public static string ToLines<T>(this IEnumerable<T> enumerable)
        {
            return ToString(enumerable, Environment.NewLine);
        }

        #endregion

        #region sublist

        /// <summary>
        /// returns all the items enumerated by an enumerable from start to end (not including end)
        /// </summary>
        /// <typeparam name="T">the type of objects in the enumerable</typeparam>
        /// <param name="enumerable">the given enumerable</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// 
        /// 
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> enumerable, int start, int end)
        {
            var index = 0;
            int count;

            // Optimise item count for ICollection interfaces.
            var list = enumerable as IList<T> ?? enumerable.ToList();
            if (enumerable is ICollection<T>)
                count = ((ICollection<T>)enumerable).Count;
            else
            {
                var collection = enumerable as ICollection;
                count = collection != null ? collection.Count : list.Count();
            }

            // Get start/end indexes, negative numbers start at the end of the enumerable.
            if (start < 0)
                start += count;

            if (end < 0)
                end += count;

            foreach (var item in list.TakeWhile(item => index < end))
            {
                if (index >= start)
                    yield return item;

                ++index;
            }
        }

        public static IEnumerable<T> Tail<T>(this IEnumerable<T> enumerable, int number)
        {
            var enumerable1 = enumerable as IList<T> ?? enumerable.ToList();
            return enumerable1.Slice(enumerable1.Count() - number, enumerable1.Count());
        }

        public static IEnumerable<T> Head<T>(this IEnumerable<T> enumerable, int number)
        {
            var enumerable1 = enumerable as IList<T> ?? enumerable.ToList();
            return enumerable1.Slice(0, number);
        }

        #endregion

        #region set-theoretic

        public static IEnumerable<T> Intersection<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
        {
            return enumerable1.Where(enumerable2.Contains);
        }

        public static IEnumerable<T> Intersection<T>(this IEnumerable<IEnumerable<T>> lists)
        {
            var enumerable = lists as IList<IEnumerable<T>> ?? lists.ToList();
            return enumerable.None() ? new List<T>() : enumerable.Inject(enumerable.ElementAt(0), (acc, list) => acc.Intersection(list));
        }

        #endregion

        #region containment

        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, params T[] args)
        {
            return args.All(enumerable.Contains);
        }

        public static bool ContainsAny<T>(this IEnumerable<T> enumerable, params T[] args)
        {
            return args.Any(enumerable.Contains);
        }

        public static bool ContainsNone<T>(this IEnumerable<T> enumerable, params T[] args)
        {
            return args.None(enumerable.Contains);
        }

        #endregion containment
    }
}