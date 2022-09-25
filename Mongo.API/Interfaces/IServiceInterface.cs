using MongoDb.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Api.Interfaces
{
    public interface IServiceInterface
    {
        public Task<List<Student>> GetAsync();
        public Task<Student> GetByIdAsync(string id);

        public Task CreateAsync(Student student);
        public Task DeleteAsync(string id);
        public Task UpdateAsync(string id, Student student);
    }
}
