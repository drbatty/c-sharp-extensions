using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpExtensions.ContainerClasses
{
    public static class DictionaryExtensions
    {
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

        #region insertion

        /// <summary>
        /// Sets the given key of a dictionary to a given value, provided that neither key nor value are null  
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's keys</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's values</typeparam>
        /// <param name="dictionary"> The dictionary whose key/value pair to set</param>
        /// <param name="key">The key to set</param>
        /// <param name="value">The value to set the key to</param>
        public static void SetIfNotNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue value)
        {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            if (key == null || value == null)
                // ReSharper restore CompareNonConstrainedGenericWithNull
                return;

            dictionary[key] = value;
        }

        public static void Set<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IDictionary<TKey, TValue> setValues)
        {
            setValues.EachKey(key => dictionary[key] = setValues[key]);
        }

        /// <summary>
        /// Returns an existing value U for key T, or creates a new instance of type U using the default constructor, 
        /// adds it to the dictionary and returns it, e.g. eg return _sessionDic.GetOrInsertNew(sessionId);
        /// </summary>
        /// <typeparam name="TKey">the type of the dictionary's keys</typeparam>
        /// <typeparam name="TValue">the type of the dictionary's values</typeparam>
        /// <param name="dictionary">the dictionary</param>
        /// <param name="key">the key</param>
        /// <returns></returns>
        public static TValue Upsert<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            if (dictionary.ContainsKey(key)) return dictionary[key];
            var newObj = new TValue();
            dictionary[key] = newObj;
            return newObj;
        }

        #endregion

        #region conversion

        public static Dictionary<string, string> ToStrings<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var result = new Dictionary<string, string>();
            dictionary.EachKey(key => result[key.ToString()] = dictionary[key].ToString());
            return result;
        }

        #endregion

        #region containment

        /// <summary>
        /// Extension that returns true if a dictionary contains the keys supplied as parameters 
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's keys</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's values</typeparam>
        /// <param name="dictionary"> The dictionary whose keys to test</param>
        /// <param name="keys"> Zero or more keys to look for</param>
        /// <returns> true if and only if the given dictionary contains the keys supplied as parameters</returns>
        public static bool ContainsKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, params TKey[] keys)
        {
            return keys.NotDefault().All(dictionary.ContainsKey);
        }

        /// <summary>
        /// Extension that returns true if a dictionary contains the values supplied as parameters 
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary's keys</typeparam>
        /// <typeparam name="TValue">The type of the dictionary's values</typeparam>
        /// <param name="dictionary"> The dictionary whose values to test</param>
        /// <param name="values"> Zero or more values to look for</param>
        /// <returns> true if and only if the given dictionary contains the values supplied as parameters</returns>
        public static bool ContainsValues<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, params TValue[] values)
        {
            return values.NotDefault().All(dictionary.ContainsValue);
        }

        #endregion

        #region lookup

        /// <summary>
        /// either gets the value for a key or returns default if the dictionary does not have the key
        /// </summary>
        /// <typeparam name="TKey">the type of the key</typeparam>
        /// <typeparam name="TValue">the type of the value</typeparam>
        /// <param name="dictionary">the dictionary to retrieve the value from</param>
        /// <param name="key">the key of the value to retrieve</param>
        /// <returns>the value for a key, or default if the dictionary does not have the key</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : default(TValue);
        }

        #endregion

        #region counting

        public static int CountKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> π)
        {
            return dictionary.Keys.Count(π);
        }

        #endregion

        #region formatting

        public static string Inspect<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(", ", dictionary.Keys.Select(key => key + "->" + dictionary[key])) + "}";
        }

        #endregion
    }
}