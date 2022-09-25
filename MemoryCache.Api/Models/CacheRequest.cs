using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryCache.Api.Models
{
    public class CacheRequest
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
