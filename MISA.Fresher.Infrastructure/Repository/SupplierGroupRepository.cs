using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    public class SupplierGroupRepository : BaseRepository<SupplierGroup>
    {
        public SupplierGroupRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
