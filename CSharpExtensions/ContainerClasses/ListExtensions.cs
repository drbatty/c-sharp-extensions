using System;
using System.Collections.Generic;

namespace CSharpExtensions.ContainerClasses
{
    public static class ListExtensions
    {
        public static IList<T> ActionAt<T>(this IList<T> list, int index, Action<T> actionAt)
        {
            actionAt(list[index]);
            return list;
        }

        /// <summary>
        /// Swap item to another place
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="list">Collection</param>
        /// <param name="indexA">ToIndex a</param>
        /// <param name="indexB">ToIndex b</param>
        /// <returns>New collection</returns>
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            var temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
            return list;
        }
    }
}