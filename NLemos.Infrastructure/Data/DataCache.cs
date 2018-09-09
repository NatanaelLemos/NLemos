using System;
using System.Collections.Generic;

namespace NLemos.Infrastructure.Data
{
    public class DataCache<T>
    {
        private readonly Dictionary<string, CacheItem<T>> _cache = new Dictionary<string, CacheItem<T>>();
        private readonly int _hoursDuration;

        public DataCache(int hoursDuration)
        {
            _hoursDuration = hoursDuration;
        }

        public T this[string key]
        {
            get
            {
                if (!_cache.ContainsKey(key)) return default(T);
                var item = _cache[key];
                if (item.Creation < DateTime.Now.AddHours(-_hoursDuration)) return default(T);
                return item.Item;
            }
            set
            {
                if (_cache.ContainsKey(key))
                {
                    _cache[key] = new CacheItem<T>(value, DateTime.Now);
                }
                else
                {
                    _cache.Add(key, new CacheItem<T>(value, DateTime.Now));
                }
            }
        }

        private class CacheItem<T>
        {
            public T Item { get; }
            public DateTime Creation { get; }

            public CacheItem(T item, DateTime creation)
            {
                Item = item;
                Creation = creation;
            }
        }
    }
}
