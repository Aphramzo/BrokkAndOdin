using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using BrokkAndOdin.Repos.interfaces;

namespace BrokkAndOdin.Repos
{
    public class RuntimeCacheRepo : ICacheRepo
    {
        static readonly ObjectCache Cache = MemoryCache.Default;
        public T Get<T>(string key)
        {
            try
            {
                return (T)Cache[key];
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public void Add(object obj, string key)
        {
            //default cache to an hour
            Add(obj, key, 1000 * 60 * 60);
        }

        public void Add(object obj, string key, int millisecondsToCache)
        {
            Cache.Add(key, obj, DateTime.Now.AddMilliseconds(millisecondsToCache));
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}