using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpExtensions.ContainerClasses
{
    public static class TupleExtensions
    {
        /// <summary>
        /// version of each which works for a tuple of lists, to iterate over a two-dimensional collection and apply an action
        /// </summary>
        /// <typeparam name="TArg1">the type of the first list</typeparam>
        /// <typeparam name="TArg2">the type of the second list</typeparam>
        /// <param name="iEnumerables">the tuple of enumerables to iterate over</param>
        /// <param name="λ">the action to apply</param>
        public static void Each<TArg1, TArg2>(this Tuple<List<TArg1>, List<TArg2>> iEnumerables,
            Action<TArg1, TArg2> λ)
        {
            iEnumerables.Item1.Each(arg1 => iEnumerables.Item2.Each(arg2 => λ(arg1, arg2)));
        }

        public static bool All<TArg1, TArg2>(this Tuple<List<TArg1>, List<TArg2>> iEnumerables,
           Func<TArg1, TArg2, bool> π)
        {
            return iEnumerables.Item1.All(arg1 => iEnumerables.Item2.All(arg2 => π(arg1, arg2)));
        }

        /// <summary>
        /// version of select which works for a pair of lists
        /// </summary>
        /// <typeparam name="TArg1">the type of object contained in the first list</typeparam>
        /// <typeparam name="TArg2">the type of object contained in the second list</typeparam>
        /// <typeparam name="TArg3">the type returned by the selector</typeparam>
        /// <param name="iEnumerables">the given pair of lists</param>
        /// <param name="func">the given selector</param>
        /// <returns></returns>
        public static IEnumerable<TArg3> Select<TArg1, TArg2, TArg3>(this Tuple<List<TArg1>, List<TArg2>> iEnumerables,
           Func<TArg1, TArg2, TArg3> λ)
        {
            return from arg1 in iEnumerables.Item1 from arg2 in iEnumerables.Item2 select λ(arg1, arg2);
        }
    }
}
