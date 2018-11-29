using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Reflection;
using System.Text;

namespace Caches
{/// <summary>
    /// .net自带缓存类
    /// </summary>
    public class dalCache : ICache
    {
        private ObjectCache cache = MemoryCache.Default;
        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Set(string key, object obj)
        {
            if (obj == null) return false;
            object lockObj = new object();
            lock (lockObj)
            {
                //List<string> userFilePath = new List<string>();
                //userFilePath.Add(@"c:\DafyWallet\cache.xml");
                //CacheItemPolicy policy = new CacheItemPolicy();
                //policy.ChangeMonitors.Add(new HostFileChangeMonitor(userFilePath));
                cache.Set(key, obj, DateTime.Now.AddHours(12));
            }
            return true;
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Set(string key, object obj, DateTime expiry)
        {
            if (obj == null) return false;
            object lockObj = new object();
            lock (lockObj)
            {
                //List<string> userFilePath = new List<string>();
                //userFilePath.Add(@"c:\DafyWallet\cache.xml");
                //CacheItemPolicy policy = new CacheItemPolicy();
                //policy.ChangeMonitors.Add(new HostFileChangeMonitor(userFilePath));
                cache.Set(key, obj, expiry);
            }
            return true;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return cache.Get(key);
        }
        /// <summary>
        /// 移出缓存
        /// </summary>
        /// <param name="key"></param>
        public bool Remove(string key)
        {
            object lockObj = new object();
            lock (lockObj)
            {
                cache.Remove(key);
            }
            return true;
        }
        /// <summary>
        /// 移出所有缓存
        /// </summary>
        /// <returns></returns>
        public void RemoveAll()
        {
            Type enumType = typeof(Caches.CacheKeys);
            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                object key = field.GetValue(enumType);
                object lockObj = new object();
                lock (lockObj)
                {
                    cache.Remove(key.ToString());
                }
            }
        }
    }
}
