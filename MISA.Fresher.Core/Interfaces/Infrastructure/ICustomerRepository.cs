using MISA.Fresher.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface Customer
    /// createdBy NHHai 28/12/2021
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Hàm lấy giá trị khách hàng
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <returns>Trả về danh sách khách hàng</returns>
        IEnumerable<Customer> Get();
        /// <summary>
        /// Hàm lấy giá trị khách hàng theo id
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <param name="entityId">mã id</param>
        /// <returns>Trả về  khách hàng theo id</returns>
        public IEnumerable<Customer> GetById(Guid entityId);
        /// <summary>
        /// Hàm lấy giá trị khách hàng theo id
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <param name="entity">khách hàng</param>
        /// <returns>Trả về  khách hàng theo id</returns>
        public int Insert(Customer entity);
        /// <summary>
        /// Hàm xóa khách hàng
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <param name="entityId">mã id</param>
        /// <returns>Trả về int</returns>
        public int Delete(Guid entityId);
        /// <summary>
        /// Hàm cập nhật khách hàng
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <param name="entityId">mã id</param>
        /// <param name="entity">khách hàng</param>
        /// <returns>Trả về int</returns>
        public int Update(Customer entity, Guid entityId);

        /// <summary>
        /// Kiểm tra mã khách hàng đã có hay chưa
        /// </summary>
        /// <param name="customerCode">mã khách hàng</param>
        /// <returns>true - đã tồn tại; false - không trùng</returns>
        /// CreatedBy: NHHai (28/12/2021)
        bool CheckCustomerCodeDuplicate(string customerCode);
    }
}
