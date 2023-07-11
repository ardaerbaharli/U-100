using System;
using System.Collections.Generic;

namespace Utils
{
    public static class DictionaryExtensions
    {
        public static void Shuffle<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var rng = new Random();
            var n = dictionary.Count;
            var keys = new List<TKey>(dictionary.Keys);
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (keys[k], keys[n]) = (keys[n], keys[k]);
            }

            var newDictionary = new Dictionary<TKey, TValue>();
            foreach (var key in keys) newDictionary.Add(key, dictionary[key]);

            dictionary.Clear();
            foreach (var keyValuePair in newDictionary) dictionary.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }
}