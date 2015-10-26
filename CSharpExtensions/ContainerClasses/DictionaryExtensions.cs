using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.Mathematical.Sets;

namespace CSharpExtensions.ContainerClasses
{
    public static class DictionaryExtensions
    {
        #region equality and equivalence

        public static bool IsEquivalentMappingTo<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue> other)
        {
            return dictionary.Keys.ToSet() == other.Keys.ToSet() &&
                   dictionary.AllKeyValues((key, value) => other[key].Equals(value));
        }

        #endregion

        #region iteration

        /// <summary>
        /// Extension to apply a given action to each key of a dictionary  
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's keys</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's values</typeparam>
        /// <param name="dictionary"> The dictionary whose keys to loop over</param>
        /// <param name="λ">The action to apply to each key</param>
        public static void EachKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey> λ)
        {
            dictionary.Keys.Each(λ);
        }

        /// <summary>
        /// Extension to return true if a predicate applies to all keys of a dictionary
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's keys</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's values</typeparam>
        /// <param name="dictionary">The dictionary whose keys to test</param>
        /// <param name="π">The predicate to test for each key</param>
        /// <returns>true if the predicate applies to all keys of a dictionary</returns>
        public static bool AllKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> π)
        {
            return dictionary.Keys.All(π);
        }

        /// <summary>
        /// Extension to apply a given action to each value of a dictionary  
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's keys</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's values</typeparam>
        /// <param name="dictionary"> The dictionary whose keys to loop over</param>
        /// <param name="λ">The action to apply to each value</param>
        public static void EachValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TValue> λ)
        {
            dictionary.Values.Each(λ);
        }

        public static bool AllValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<TValue, bool> π)
        {
            return dictionary.Values.All(π);
        }

        public static void EachKeyValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> λ)
        {
            dictionary.EachKey(key => λ(key, dictionary[key]));
        }

        public static bool AllKeyValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, bool> π)
        {
            return dictionary.AllKeys(key => π(key, dictionary[key]));
        }

        public static TAcc InjectKeyValues<TAcc, TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TAcc acc, Func<TAcc, TKey, TValue, TAcc> λ)
        {
            if (λ == null)
                return acc;
            var accumulator = acc;
            dictionary.EachKeyValue((k, v) => accumulator = λ(accumulator, k, v));
            return accumulator;
        }

        public static IEnumerable<T> SelectKeyValues<T, TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Func<TKey, TValue, T> λ)
        {
            return dictionary.Keys.Select(key => λ(key, dictionary[key]));
        }

        #endregion

        #region restriction, image and preimage

        public static IDictionary<TKey, TValue> Restrict<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Set<TKey> keys)
        {
            return (keys & dictionary.Keys.ToSet()).ToDictionary(k => k, k => dictionary[k]);
        }

        public static Set<TValue> Image<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Set<TKey> keys)
        {
            return dictionary.Restrict(keys).Values.ToSet();
        }

        public static Set<TKey> Preimage<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Set<TValue> values)
        {
            return dictionary.Keys.Where(k => dictionary[k] < values).ToSet();
        }

        public static IDictionary<TKey, TValue> Restrict<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            Set<TValue> values)
        {
            return dictionary.Restrict(dictionary.Preimage(values));
        }

        #endregion
    }
}