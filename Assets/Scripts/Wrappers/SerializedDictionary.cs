using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OmniDi.Library.Wrappers
{
    public abstract class DrawableDictionary
    {

    }

    [Serializable]
    public class SerializedDictionary<TKey, TValue> : DrawableDictionary, IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private TKey[] keys;
        [SerializeField] private TValue[] values;


        [NonSerialized]
        private Dictionary<TKey, TValue> _dict;
        [NonSerialized]
        private IEqualityComparer<TKey> _comparer;

        public IEqualityComparer<TKey> Comparer => _comparer;
        public int Count => _dict?.Count ?? 0;

        public ICollection<TKey> Keys
        {
            get
            {
                _dict ??= new Dictionary<TKey, TValue>(_comparer);
                return _dict.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                _dict ??= new Dictionary<TKey, TValue>(_comparer);
                return _dict.Values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (_dict == null) throw new KeyNotFoundException();
                return _dict[key];
            }
            set
            {
                _dict ??= new Dictionary<TKey, TValue>(_comparer);
                _dict[key] = value;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        public SerializedDictionary()
        {

        }

        public SerializedDictionary(IEqualityComparer<TKey> comparer)
        {
            _comparer = comparer;
        }

        public SerializedDictionary(SerializedDictionary<TKey, TValue> dictionary)
        {
            _dict = new Dictionary<TKey, TValue>(dictionary._dict);
        }


        public void Add(TKey key, TValue value)
        {
            _dict ??= new Dictionary<TKey, TValue>(_comparer);
            _dict.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _dict != null && _dict.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return _dict != null && _dict.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_dict != null) return _dict.TryGetValue(key, out value);
            value = default;
            return false;
        }


        public void Clear()
        {
            _dict?.Clear();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dict ??= new Dictionary<TKey, TValue>(_comparer);
            (_dict as ICollection<KeyValuePair<TKey, TValue>>).Add(item);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dict != null && (_dict as ICollection<KeyValuePair<TKey, TValue>>).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            (_dict as ICollection<KeyValuePair<TKey, TValue>>)?.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dict != null && (_dict as ICollection<KeyValuePair<TKey, TValue>>).Remove(item);
        }


        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return _dict?.GetEnumerator() ?? default(Dictionary<TKey, TValue>.Enumerator);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict?.GetEnumerator() ?? Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return _dict?.GetEnumerator() ?? Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
        }


        public void OnAfterDeserialize()
        {
            if(keys != null && values != null)
            {
                if (_dict == null) _dict = new Dictionary<TKey, TValue>(keys.Length, _comparer);
                else _dict.Clear();
                for(int i = 0; i < keys.Length; i++)
                {
                    if (i < values.Length)
                        _dict[keys[i]] = values[i];
                    else
                        _dict[keys[i]] = default;
                }
            }

            keys = null;
            values = null;
        }

        public void OnBeforeSerialize()
        {
            if(_dict == null || _dict.Count == 0)
            {
                keys = null;
                values = null;
            }
            else
            {
                var count = _dict.Count;
                keys = new TKey[count];
                values = new TValue[count];
                var i = 0;
                using var enumerator = _dict.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    keys[i] = enumerator.Current.Key;
                    values[i] = enumerator.Current.Value;
                    i++;
                }
            }
        }
    }
}