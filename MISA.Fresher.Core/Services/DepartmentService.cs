using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Services
{
    /// <summary>
    /// department service
    /// createdBy NHHAi 1/1/2022
    /// </summary>
    public class DepartmentService : BaseService<Department>
    {
        public DepartmentService(IBaseRepository<Department> baseRepository) : base(baseRepository)
        {

        }
    }
}
