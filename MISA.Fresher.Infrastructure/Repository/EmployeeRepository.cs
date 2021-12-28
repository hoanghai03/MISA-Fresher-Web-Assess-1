using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public int Insert(Employee entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Employee entity, Guid entityId)
        {
            throw new NotImplementedException();
        }
    }
}
