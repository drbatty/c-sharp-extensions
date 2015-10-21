using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CSharpExtensions
{
    public static class GenericExtensions
    {
        #region containment

        /// <summary>
        /// Extension which returns true if the supplied generic object is equal to at least one of the supplied parameters
        /// (syntactic sugar)
        /// </summary>
        /// <typeparam name="T">generic type to test equality on</typeparam>
        /// <param name="t">The generic object for equality testing</param>
        /// <param name="values"> Zero or more objects of the supplied generic type, for comparision</param>
        /// <returns> true if and only if the supplied generic object is equal to at least one of the supplied parameters </returns>
        public static bool In<T>(this T t, params T[] values)
        {
            return values.Any(x => t.Equals(x));
        }

        /// <summary>
        /// Extension which returns true if the supplied generic object is not equal to any of the supplied parameters
        /// (syntactic sugar)
        /// </summary>
        /// <typeparam name="T">generic type to test equality on</typeparam>
        /// <param name="t">The generic object for equality testing</param>
        /// <param name="values"> Zero or more objects of the supplied generic type, for comparision</param>
        /// <returns> true if and only if the supplied generic object is not equal to any of the supplied parameters </returns>

        public static bool NotIn<T>(this T t, params T[] values)
        {
            return !t.In(values);
        }

        public static T Clamp<T>(this T source, T start, T end) where T : IComparable //50.Clamp( 0, 20 ); // = 20
        {
            var isReversed = start.CompareTo(end) > 0;
            var smallest = isReversed ? end : start;
            var biggest = isReversed ? start : end;

            return source.CompareTo(smallest) < 0
                ? smallest
                : source.CompareTo(biggest) > 0
                    ? biggest
                    : source;
        }

        public static bool Between<T>(this T value, T from, T to) where T : IComparable<T>
        {
            return value.CompareTo(@from) >= 0 && value.CompareTo(to) <= 0;
        }

        #endregion

        #region chaining

        /// <summary>
        /// Extension which applies an action to a parameter t then returns the parameter, thus stopping the parameter being "swallowed up" by the action.
        /// Can be used for tidying code, such as in tests, by reducing two or three lines of code to one. 
        /// Debatable whether it improves readability ...
        /// </summary>
        /// <typeparam name="T">the parameter type for the action</typeparam>
        /// <param name="t">The parameter for the action</param>
        /// <param name="λ"> The action to apply to the parameter </param>
        /// <returns> the supplied parameter </returns>
        public static T Return<T>(this T t, Action<T> λ)
        {
            λ(t);
            return t;
        }

        #endregion

        #region conversion

        public static List<T> WrapInList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static string ToXml<T>(this T obj) where T : class
        {
            var s = new XmlSerializer(obj.GetType());
            using (var writer = new StringWriter())
            {
                s.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        #endregion

        #region tuple handling

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

        /// <summary>
        /// Extension which returns a triple of generic objects by combining 
        /// the given object with two other supplied objects. Saves having to write
        /// new Tuple ...
        /// </summary>
        /// <typeparam name="T1">the type of the first object in the triple</typeparam>
        /// <typeparam name="T2">the type of the second object in the triple</typeparam>
        /// <typeparam name="T3">the type of the third object in the triple</typeparam>
        /// <param name="t1">the first object in the triple</param>
        /// <param name="t2">the second object in the triple </param>
        /// <param name="t3">the third object in the triple </param>
        /// <returns> a tuple of three objects (triple) containing all of the supplied objects </returns>
        public static Tuple<T1, T2, T3> Triple<T1, T2, T3>(this T1 t1, T2 t2, T3 t3)
        {
            return new Tuple<T1, T2, T3>(t1, t2, t3);
        }

        #endregion

        #region null handling

        public static TResult DefaultIfNull<TArg, TResult>(this TArg t, Func<TArg, TResult> λ) where TArg : class
        {
            return t == null ? default(TResult) : λ(t);
        }

        public static string ToStringOrEmpty<T>(this T t) where T : class
        {
            return t == null ? "" : t.ToString();
        }

        #endregion
    }
}