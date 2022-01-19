using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    /// <summary>
    /// interface baservice
    /// createdBy NHHai 31/12/2021
    /// </summary>
    public interface IBaseService<T>
    {
        /// <summary>
        /// Hàm validate khi lấy toàn bộ dữ liệu từ Entity
        /// </summary>
        /// <returns>trả về danh sách entity</returns>
        /// createdBy NHHai 31/12/2021
        public IEnumerable<T> GetService();

        /// <summary>
        /// Hàm validate khi lấy dữ liệu nhân viên với id
        /// </summary>
        /// <param name="entityId">mã id</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 31/12/2021
        public T GetByIdService(Guid entityId);

        /// <summary>
        /// Hàm validate khi xóa nhân viên
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>trả vê :</returns>
        /// createdBy NHHai 31/12/2021
        public int DeleteService(Guid entityId);

        /// <summary>
        /// hàm validate khi thêm mới entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 28/12/2021
        public int? InsertService(T entity);

        /// <summary>
        /// Hàm validate khi cập nhật entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="entityId">mã id</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 28/12/2021
        public int? UpdateService(T entity, Guid entityId);


    }
}
