using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    /// <summary>
    /// interface customer service
    /// createdBy NHHai 28/12/2021
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// hàm thêm mới khách hàng
        /// </summary>
        /// <param name="entity">khách hàng</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 28/12/2021
        public int? Insert(Customer entity);

        /// <summary>
        /// cập nhật khách hàng
        /// </summary>
        /// <param name="entity">khách hàng</param>
        /// <param name="entityId">mã khách hàng</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 28/12/2021
        public int? Update(Customer entity, Guid entityId);


    }
}
