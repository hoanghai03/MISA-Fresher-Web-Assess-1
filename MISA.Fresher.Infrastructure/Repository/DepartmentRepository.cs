using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    /// <summary>
    /// Department Repository
    /// createdBy NHHai 1/1/2022
    /// </summary>
    public class DepartmentRepository : BaseRepository<Department>
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
