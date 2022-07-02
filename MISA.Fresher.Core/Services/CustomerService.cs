using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Services
{
    /// <summary>
    /// customer service
    /// createdBy NHHai 28/12/2021
    /// </summary>
    public class CustomerService : BaseService<Customer>
    {
        public CustomerService(IBaseRepository<Customer> baseRepository):base(baseRepository,null)
        {

        }
    }
}
