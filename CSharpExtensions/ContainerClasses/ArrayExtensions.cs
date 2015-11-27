using System;
using System.Collections.Generic;

namespace CSharpExtensions.ContainerClasses
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Extension to remove the first element from an array. Only use when you have to use arrays not lists.
        /// Not particularly efficient as it converts to a list and back again so be aware of the overheads.
        /// </summary>
        /// <typeparam name="T">The type of objects contained in the array</typeparam>
        /// <param name="array">The two-parameter action to project</param>
        /// <returns> a new array which is the original array with the first element removed</returns>
        public static T[] RemoveFirstEntry<T>(this T[] array)
        {
            if (array.Length <= 0) return array;
            var list = new List<T>(array);
            list.RemoveAt(0);
            return list.ToArray();
        }

        /// <summary>
        /// looping extension to iterate over every index of an array and perform an action, similar to Each for an enumerable
        /// </summary>
        /// <typeparam name="T">the type of objects contained in the array</typeparam>
        /// <param name="array">the array to iterate over</param>
        /// <param name="λ">the action to perform</param>
        public static void EachIndex<T>(this T[] array, Action<int> λ)
        {
            if (λ == null)
                return;
            array.Length.Do(λ);
        }

        ///<summary>
        ///	Check if the index is within the array
        ///</summary>
        ///<param name = "source"></param>
        ///<param name = "index"></param>
        ///<returns></returns>
        public static bool HasIndex(this Array source, int index)
        {
            return source != null && index >= 0 && index < source.Length;
        }
    }
}