using MemoryCache.Api.Interfaces;
using MemoryCache.Api.Models;
using MemoryCache.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MemoryCache.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryCacheController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IMemoryCacheService memoryCacheService;

        //private readonly IMemoryCacheService memoryCacheService;
        protected IOptions<MyStoreDatabase> options;

        public MemoryCacheController(IMemoryCache memoryCache,IMemoryCacheService memoryCacheService)
        {
            this.memoryCache = memoryCache;
            this.memoryCacheService = memoryCacheService;
        }

        [HttpGet("{key}")]
        public IActionResult GetCache(string key)
        {
            string value = String.Empty;
            memoryCache.TryGetValue(key, out value);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult SetCache(CacheRequest data)
        {
            var cacheOptions = new MemoryCacheEntryOptions {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024
            };

            memoryCache.Set(data.Key, data.Value,cacheOptions);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "studentList";
            if (!memoryCache.TryGetValue(cacheKey, out object value))
            {
                value = await memoryCacheService.GetAllAsync();

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024
                };

                memoryCache.Set(cacheKey, value, cacheOptions);
            }
            return Ok(value);
        }

        [HttpPost("insert_many")]
        public async Task<IActionResult> InsertMany()
        {
            await memoryCacheService.CreateMultipleAsync();
            return Ok();
        }
    }
}
