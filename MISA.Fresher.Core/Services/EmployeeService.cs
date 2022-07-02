using Microsoft.AspNetCore.Http;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Fresher.Core.Enum;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using MISA.Fresher.Core.MISAAttribute;

namespace MISA.Fresher.Core.Services
{
    /// <summary>
    /// employee service
    /// createdBy NHHAi 28/12/2021
    /// </summary>
    public class EmployeeService : BaseService<Employee>, IEmployeeService

    {

        IBaseRepository<Employee> _employee;
        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeRepository employee) : base(employeeRepository,null)
        {
            _employee = employee;
        }

        public string GetNewEmployeeCode()
        {
            try
            {
                // gọi đến làm lấy mã code mới ở repository
                var employeeCode = _employee.GetMaxCodeRepository();
                employeeCode = employeeCode.Substring(3, employeeCode.Length - 3);
                //int number = Int32.Parse(employeeCode);
                Int32.TryParse(employeeCode, out int number);

                number += 1;
                var code = "NV-" + number.ToString().Trim();
                // trả về mã code mới
                return code;
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

    }
}
