using Microsoft.Extensions.Options;
using MongoDb.Api.Interfaces;
using MongoDb.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Api.Services
{
    public class StudentService : IServiceInterface
    {
        private readonly IMongoCollection<Student> _studentsCollection;
        public StudentService(IOptions<MyStoreDatabase> connectDb)
        {
            var mongoClient = new MongoClient(connectDb.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(connectDb.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<Student>(connectDb.Value.ProductsCollectionName);
        }

        public async Task<List<Student>> GetAsync() {
            var res = await _studentsCollection.Find(_ => true).ToListAsync();
            return res;
        }
        
        public async Task<Student?> GetByIdAsync(string id)
        {
            var res = await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return res;
        }

        public async Task CreateAsync(Student student)
        {
            await _studentsCollection.InsertOneAsync(student);
        } 
        public async Task DeleteAsync(string id)
        {
            await _studentsCollection.DeleteOneAsync(x => x.Id == id);
        } 
        public async Task UpdateAsync(string id,Student student)
        {
            await _studentsCollection.ReplaceOneAsync(x => x.Id == id,student);
        } 
    }
}
