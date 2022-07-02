using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Caching
{
    public static class MemoryCacheHelper
    {
        public static MemoryCache memoryCache { get; set; } = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// Mặc định cache trong 1d
        /// </summary>
        public static double defaultCacheTime = 60 * 24;

        public static void Set(string key, object value, double? cacheTime = default)
        {
            if (cacheTime == null)
            {
                memoryCache.Set(key, value, TimeSpan.FromMinutes(defaultCacheTime));
            }
            else
            {
                memoryCache.Set(key, value, TimeSpan.FromMinutes(cacheTime.Value));
            }
        }

        public static object Get(string key)
        {
            return memoryCache.Get(key);
        }
    }
}
