using System.Collections.Generic;

namespace CSharpExtensions.ContainerClasses
{
    public static class CollectionExtensions
    {
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