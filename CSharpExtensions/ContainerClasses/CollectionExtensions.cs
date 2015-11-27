using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpExtensions.ContainerClasses
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// returns true if a given iCollection contains all of the elements of another iCollection
        /// </summary>
        /// <typeparam name="T">The type contained in the gicen iCollection</typeparam>
        /// <typeparam name="S">The type of the contained elements, which extends ICollection of T</typeparam>
        /// <param name="iCollection">The given iCollection</param>
        /// <param name="other">The other iCollection to test for containment</param>
        /// <returns></returns>
        public static bool Contains<T, S>(this ICollection<T> iCollection, S other)
            where S : ICollection<T>
        {
            return other.All(iCollection.Contains);
        }

        /// <summary>
        /// adds zero or more values to a given iCollection
        /// </summary>
        /// <typeparam name="T">the type contained in the given collection</typeparam>
        /// <typeparam name="S">the type of the values to add, which must be a subtype of T</typeparam>
        /// <param name="iCollection">the collection to add to</param>
        /// <param name="values">the values to add</param>
        public static void AddMany<T, S>(this ICollection<T> iCollection, params S[] values)
            where S : T
        {
            values.Each(value => iCollection.Add(value));
        }

        /// <summary>
        /// removes all of the elements enumerated by an enumerable from a given collection
        /// </summary>
        /// <typeparam name="T">the type contained in the collection</typeparam>
        /// <typeparam name="S">the type enumerated by the enumerable, which must extend T</typeparam>
        /// <param name="iCollection">the given collection</param>
        /// <param name="iEnumerable">the given enumerable</param>
        public static bool Remove<T, S>(this ICollection<T> iCollection, IEnumerable<S> iEnumerable)
            where S : T
        {
            return iEnumerable != null && iEnumerable.All(t => iCollection.Remove(t));
        }

        /// <summary>
        /// remove all elements from a collection which satisfy a given predicate
        /// </summary>
        /// <typeparam name="T">the type of objects contained in the collection</typeparam>
        /// <param name="iCollection">the given collection</param>
        /// <param name="predicate"></param>
        public static void Remove<T>(this ICollection<T> iCollection, Func<T, bool> π)
        {
            if (π == null)
                return;
            iCollection.Where(π).ToList().Each(item => iCollection.Remove(item));
        }


        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (var item in source)
                destination.Add(item);
        }

        public static void AddRanges<T>(this ICollection<T> iCollection, params IEnumerable<T>[] ranges)
        {
            ranges.Each(iCollection.AddRange);
        }

    }
}