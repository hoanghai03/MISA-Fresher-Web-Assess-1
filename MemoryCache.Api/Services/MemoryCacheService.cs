using MemoryCache.Api.Interfaces;
using MemoryCache.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryCache.Api.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMongoCollection<Student> _studentCollection;

        public MemoryCacheService(IOptions<MyStoreDatabase> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _studentCollection = mongoDatabase.GetCollection<Student>(options.Value.ProductsCollectionName);

        }

        public async Task CreateMultipleAsync()
        {
            List<Student> students = new List<Student>();
            for (int i=0;i<=10000;i++)
            {
                Student student = new Student();
                student.Name = i + "";
                student.Age = i;
                student.Address = "Nghệ An";
                students.Add(student);
            }
            await _studentCollection.InsertManyAsync(students);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var res = await _studentCollection.Find(_ => true).ToListAsync();
            return res;
        }
    }
}
