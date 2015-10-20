using System;
using System.Collections.Generic;

namespace CSharpExtensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Extension which returns a pair of generic objects by combining 
        /// the given object with another supplied object. Saves having to write
        /// new Tuple ...
        /// </summary>
        /// <typeparam name="T1">the type of the first object in the triple</typeparam>
        /// <typeparam name="T2">the type of the second object in the triple</typeparam>
        /// <param name="t1">the first object in the triple</param>
        /// <param name="t2">the second object in the triple </param>
        /// <returns> a tuple of two objects (pair) containing both of the supplied objects </returns>
        public static Tuple<T1, T2> Pair<T1, T2>(this T1 t1, T2 t2)
        {
            return new Tuple<T1, T2>(t1, t2);
        }

        public static List<T> WrapInList<T>(this T item)
        {
            return new List<T> { item };
        }
    }
}
