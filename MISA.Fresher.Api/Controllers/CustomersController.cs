using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    /// <summary>
    /// controller khách hàng
    /// createdBy NHHai 31/12/2021
    /// </summary>
    public class CustomersController: BaseController<Customer>
    {
        public CustomersController(IBaseService<Customer> baseService) : base(baseService)
        {
        }
    }
}
