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
    /// controller Phòng ban
    /// createdBy NHHAi 1/1/2022
    /// </summary>
    public class DepartmentsController : BaseController<Department>
    {
        public DepartmentsController(IBaseService<Department> baseService) : base(baseService)
        {
        }
    }
}
