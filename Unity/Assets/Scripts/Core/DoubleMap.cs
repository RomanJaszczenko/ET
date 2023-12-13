using System;
using System.Collections.Generic;

namespace ET
{
    public class DoubleMap<K, V>
    {
        private readonly Dictionary<K, V> kv = new();
        private readonly Dictionary<V, K> vk = new();

        public DoubleMap()
        {
        }

        public DoubleMap(int capacity)
        {
            kv = new Dictionary<K, V>(capacity);
            vk = new Dictionary<V, K>(capacity);
        }

        public void ForEach(Action<K, V> action)
        {
            foreach (var (key, value) in kv)
            {
                action?.Invoke(key, value);
            }
        }

        public List<K> Keys => new List<K>(kv.Keys);
        public List<V> Values => new List<V>(vk.Keys);

        public void Add(K key, V value)
        {
            if (key == null || value == null || kv.ContainsKey(key) || vk.ContainsKey(value))
            {
                return;
            }

            kv.Add(key, value);
            vk.Add(value, key);
        }

        public V GetValueByKey(K key) => key != null && kv.TryGetValue(key, out var value) ? value : default;

        public K GetKeyByValue(V value) => value != null && vk.TryGetValue(value, out var key) ? key : default;

        public void RemoveByKey(K key)
        {
            if (key == null || !kv.TryGetValue(key, out var value))
            {
                return;
            }

            kv.Remove(key);
            vk.Remove(value);
        }

        public void RemoveByValue(V value)
        {
            if (value == null || !vk.TryGetValue(value, out var key))
            {
                return;
            }

            kv.Remove(key);
            vk.Remove(value);
        }

        public void Clear()
        {
            kv.Clear();
            vk.Clear();
        }

        public bool ContainsKey(K key) => key != null && kv.ContainsKey(key);

        public bool ContainsValue(V value) => value != null && vk.ContainsKey(value);

        public bool Contains(K key, V value) => key != null && value != null && kv.ContainsKey(key) && vk.ContainsKey(value);
    }
}
