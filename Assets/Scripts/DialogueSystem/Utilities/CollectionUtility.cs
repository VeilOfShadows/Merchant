using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionUtility
{
    public static void AddItem<K, V>(this SerializableDictionary<K, List<V>> serializableDictionary, K key, V value) 
    {
        if (serializableDictionary.ContainsKey(key))
        {
            serializableDictionary[key].Add(value);

            return;
        }

        serializableDictionary.Add(key, new List<V>() { value });
    }
}
