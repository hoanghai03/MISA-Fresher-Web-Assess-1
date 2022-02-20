using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    /// <summary>
    /// Controller nhà cung cấp
    /// createdBy NHHai 13/2/2021
    /// </summary>
    public class SuppliersController : BaseController<Supplier>
    {
        ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService) : base(supplierService)
        {
            _supplierService = supplierService;
    }

        /// <summary>
        /// hàm lấy mã code mới của nhân viên
        /// </summary>
        /// <returns>trả về mã code mới</returns>
        /// createdBy NHHAi 2/1/2022
        [HttpGet("NewSupplierCode")]
        public string GetNewSupplierCode()
        {
            try
            {
                // Hàm gọi đến hàm lấy mã nhân viên ở service
                var newEmployeeCode = _supplierService.GetNewSupplierCode();
                // trả về mã mới
                return newEmployeeCode;
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }
    }
}
