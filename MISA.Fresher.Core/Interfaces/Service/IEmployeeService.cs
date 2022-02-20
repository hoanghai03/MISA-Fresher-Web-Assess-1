using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    /// <summary>
    /// interface employee service
    /// createdBy NHHAi 28/12/2021
    /// </summary>
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Hàm gọi đến repository để lấy mã nhân viên mới
        /// </summary>
        /// <returns>Trả về mã nhân viên mới</returns>
        /// createdBy NHHAi 2/1/2022
        public string GetNewEmployeeCode();
        
    }
}
