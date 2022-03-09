using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    /// <summary>
    /// SupplierRepository
    /// createdBy NHHai 25/2/2022
    /// </summary>
    public class SupplierRepository:BaseRepository<Supplier>,ISupplierRepository
    {
        public SupplierRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
