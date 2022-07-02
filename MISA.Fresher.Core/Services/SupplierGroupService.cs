﻿using MISA.Fresher.Core.Entities;
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
    /// SupplierGroupService
    /// createdBy NHHAi 20/2/2022
    /// </summary>
    public class SupplierGroupService:BaseService<SupplierGroup>,ISupplierGroupService
    {
        public SupplierGroupService(IBaseRepository<SupplierGroup> baseRepository) : base(baseRepository,null)
        {

        }
    }
}
