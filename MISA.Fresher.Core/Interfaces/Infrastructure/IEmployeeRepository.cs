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
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Hàm lấy giá trị nhân viên
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <returns>Trả về danh sách nhân viên</returns>
        IEnumerable<Employee> Get();
        /// <summary>
        /// Hàm lấy giá trị nhân viên theo id
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        ///  <param name="entityId">mã id</param>
        /// <returns>Trả về  nhân viên theo id</returns>
        public IEnumerable<Employee> GetById(Guid entityId);
        /// <summary>
        /// Hàm lấy giá trị nhân viên theo id
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <param name="entity">nhân viên</param>
        /// <returns>Trả về  nhân viên theo id</returns>
        public int Insert(Employee entity);
        /// <summary>
        /// Hàm xóa nhân viên
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <param name="entityId">mã id</param>
        /// <returns>Trả về int</returns>
        public int Delete(Guid entityId);
        /// <summary>
        /// Hàm cập nhật nhân viên
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        ///<param name="entityId">mã id</param>
        /// <param name="entity">nhân viên</param>
        /// <returns>Trả về int</returns>
        public int Update(Employee entity, Guid entityId);
    }
}
