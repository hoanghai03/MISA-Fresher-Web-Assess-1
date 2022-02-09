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

        /// <summary>
        /// Hàm phân trang dữ liệu
        /// </summary>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">vị trí trang</param>
        /// <param name="employeeFilter">chuỗi filter</param>
        /// <returns>trả về số bản ghi của trang</returns>
        /// createdBy NHHAi 10/1/2021
        [HttpGet("filter")]
        public IActionResult Filter(int pageSize,int pageNumber,string employeeFilter)
        {
            try
            {
                // Hàm gọi đến hàm lấy mã nhân viên ở service
                var filter = _employee.FilterService(pageSize, pageNumber, employeeFilter);
                // trả về mã mới
                return Ok(filter);
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Data);

            }
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="employeeIds">danh sách id cần xóa</param>
        /// <returns></returns>
        /// createdBY NHHai 10/1/2021
        [HttpDelete("all")]
        public IActionResult DeleteAll(List<string> employeeIds)
        {
            try
            {
                // thực hiện gọi đến service
                return Ok(_employee.DeleteAllService(employeeIds));
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Hàm xuất khẩu dữ liệu ra excel
        /// </summary>
        /// <param name="pageSize">kích thước trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="employeeFilter">filter</param>
        /// <returns>file excel</returns>
        /// createdBy NHHAi 20/1/2022
        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            try
            {
                var res = _employee.ExportListUsingEPPlus();
                // đặt tên file excel
                string excelName = $"Danh-sách-nhân-viên-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                // file
                return File(res, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }
    }
}
