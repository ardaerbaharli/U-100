using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class DictionaryUnity<K, V> : IEnumerable
{
    public List<KeyValuePairUnity<K, V>> dictionary;

    public DictionaryUnity()
    {
        dictionary = new List<KeyValuePairUnity<K, V>>();
    }

    public V this[K key]
    {
        get
        {
            foreach (var pair in dictionary)
            {
                if (!pair.key.Equals(key)) continue;
                return pair.value;
            }

            return default;
        }
        set
        {
            foreach (var pair in dictionary)
            {
                if (!pair.key.Equals(key)) continue;
                pair.value = value;
                break;
            }
        }
    }

    // Keys
    public List<K> Keys
    {
        get
        {
            var keys = new List<K>();
            foreach (var pair in dictionary) keys.Add(pair.key);

            return keys;
        }
    }

    // Values
    public List<V> Values
    {
        get
        {
            var values = new List<V>();
            foreach (var pair in dictionary) values.Add(pair.value);

            return values;
        }
    }


    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(K key)
    {
        foreach (var pair in dictionary)
        {
            if (!pair.key.Equals(key)) continue;
            return true;
        }

        return false;
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key)) return;
        dictionary.Add(new KeyValuePairUnity<K, V>(key, value));
    }
}

[Serializable]
public class KeyValuePairUnity<K, V>
{
    public K key;
    public V value;

    public KeyValuePairUnity(K key, V value)
    {
        this.key = key;
        this.value = value;
    }
}