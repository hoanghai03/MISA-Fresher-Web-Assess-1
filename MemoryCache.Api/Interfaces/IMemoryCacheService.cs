using MemoryCache.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryCache.Api.Interfaces
{
    public interface IMemoryCacheService
    {
        public Task<List<Student>> GetAllAsync();

        public Task CreateMultipleAsync();
    }
}
