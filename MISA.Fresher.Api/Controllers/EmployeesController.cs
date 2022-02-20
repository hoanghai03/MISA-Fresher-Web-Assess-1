using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Enum;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace MISA.Fresher.Api.Controllers
{
    /// <summary>
    /// controller nhân viên
    /// createdBy NHHai 31/12/2021
    /// </summary>
    public class EmployeesController : BaseController<Employee>
    {
        IEmployeeService _employee;
        public EmployeesController(IEmployeeService employeeService, IEmployeeService employee) : base(employeeService)
        {
            _employee = employee;
        }
        /// <summary>
        /// hàm lấy mã code mới của nhân viên
        /// </summary>
        /// <returns>trả về mã code mới</returns>
        /// createdBy NHHAi 2/1/2022
        [HttpGet("NewEmployeeCode")]
        public string GetNewEmployeeCode()
        {
            try
            {
                // Hàm gọi đến hàm lấy mã nhân viên ở service
                var newEmployeeCode = _employee.GetNewEmployeeCode();
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
