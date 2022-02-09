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
        
        /// <summary>
        ///     Hàm phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi</param>
        /// <param name="pageNumber">vị trí trang</param>
        /// <param name="employeeFilter">chuỗi</param>
        /// <returns></returns>
        /// createdBy NHHai 8/1/2022
        public DataFilter FilterService(int pageSize, int pageNumber, string employeeFilter);

        /// <summary>
        /// Hàm validate dữ liệu và gọi đến repository
        /// </summary>
        /// <param name="employeeIds">danh sách id</param>
        /// <returns></returns>
        /// createdBy NHHAi 10/1/2022
        public int DeleteAllService(List<string> employeeIds);

        /// <summary>
        /// Hàm xuất excel
        /// </summary>
        /// <returns>stream</returns>
        /// createdBy NHHAi 19/1/2022
        public Stream ExportListUsingEPPlus();
    }
}
