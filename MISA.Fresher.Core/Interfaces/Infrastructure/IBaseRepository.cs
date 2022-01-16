using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface base repository
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// createdBy NHHAi 31/12/2021
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Hàm lấy toàn bộ giá trị của entity T
        /// createdBy NHHAi 28/12/2021
        /// </summary>
        /// <returns>Trả về danh sách entity</returns>
        public IEnumerable<T> Get();

        /// <summary>
        /// Hàm lấy giá trị entity theo id
        /// </summary>
        ///  <param name="entityId">mã id</param>
        /// <returns>Trả về entity theo id</returns>
        /// createdBy NHHAi 28/12/2021
        public T GetById(Guid entityId);

        /// <summary>
        /// Hàm thêm dữ liệu entity 
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>Trả về 201: nếu thêm dữ liệu thành công</returns>
        /// createdBy NHHAi 28/12/2021
        public int Insert(T entity);

        /// <summary>
        /// Hàm xóa entity
        /// </summary>
        /// <param name="entityId">mã id</param>
        /// <returns>Trả về 1 </returns>
        /// createdBy NHHAi 28/12/2021
        public int Delete(Guid entityId);

        /// <summary>
        /// Hàm cập nhật entity
        /// </summary>
        ///<param name="entityId">mã id</param>
        /// <param name="entity">entity</param>
        /// <returns>Trả về int</returns>
        /// createdBy NHHAi 28/12/2021
        public int Update(T entity, Guid entityId);

        /// <summary>
        /// Hàm kiểm tra trùng mã code
        /// </summary>
        /// <param name="entityCode">mã code của table</param>
        /// <returns>trả về: true- không trùng;false - trùng</returns>
        /// createdBy NHHai 1/1/2021
        public int CheckCodeDuplicate(string entityCode);

        public string GetCode(Guid? entityId);

        
    }
}
