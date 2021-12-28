using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    /// <summary>
    /// interface employee service
    /// createdBy NHHAi 28/12/2021
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// hàm thêm mới nhân viên
        /// </summary>
        /// <param name="entity">nhân viên</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 28/12/2021
        public int? Insert(Employee entity);

        /// <summary>
        /// cập nhật nhân viên
        /// </summary>
        /// <param name="entity">nhân viên</param>
        /// <param name="entityId">mã nhân viên</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 28/12/2021
        public int? Update(Employee entity, Guid entityId);
    }
}
