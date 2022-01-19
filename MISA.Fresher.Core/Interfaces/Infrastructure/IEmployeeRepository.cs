using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface Employee
    /// createdBy NHHai 28/12/2021
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy mã lớn nhất trong database
        /// </summary>
        /// <returns>trả về mã lớn nhất trong db</returns>
        /// createdBy NHHAi 2/1/2022
        public string GetMaxCodeRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// <returns></returns>
        public DataFilter FilterRepository(int pageSize, int pageNumber, string employeeFilter);


        /// <summary>
        /// Hàm xóa nhiều dữ liệu
        /// </summary>
        /// <param name="employeeIds">danh sách id</param>
        /// <returns></returns>
        /// createdBy NHHai 10/1/2022
        public int DeleteAllRepository(List<string> employeeIds);
    }
}
