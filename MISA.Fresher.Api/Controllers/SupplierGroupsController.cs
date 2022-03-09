using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    /// <summary>
    /// SupplierGroupController
    /// createdBy NHHai 20/2/2022
    /// </summary>
    public class SupplierGroupsController : BaseController<SupplierGroup>
    {
        public SupplierGroupsController(IBaseService<SupplierGroup> baseService) : base(baseService)
        {
        }
    }
}
