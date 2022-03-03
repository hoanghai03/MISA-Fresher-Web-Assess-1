using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ServiceResult GetService();

        /// <summary>
        /// Hàm validate khi lấy dữ liệu nhân viên với id
        /// </summary>
        /// <param name="entityId">mã id</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 31/12/2021
        public ServiceResult GetByIdService(Guid entityId);

        /// <summary>
        /// Hàm validate khi xóa nhân viên
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>trả vê :</returns>
        /// createdBy NHHai 31/12/2021
        public ServiceResult DeleteService(Guid entityId);

        /// <summary>
        /// hàm validate khi thêm mới entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 28/12/2021
        public ServiceResult InsertService(T entity);

        /// <summary>
        /// Hàm validate khi cập nhật entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="entityId">mã id</param>
        /// <returns>trả về int</returns>
        /// createdBy NHHai 28/12/2021
        public ServiceResult UpdateService(T entity, Guid entityId);

        /// <summary>
        ///     Hàm phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi</param>
        /// <param name="pageNumber">vị trí trang</param>
        /// <param name="filter">chuỗi</param>
        /// <returns></returns>
        /// createdBy NHHai 8/1/2022
        public ServiceResult FilterService(PaginationRequest paginationRequest);

        /// <summary>
        /// Hàm validate dữ liệu và gọi đến repository
        /// </summary>
        /// <param name="entityIds">danh sách id</param>
        /// <returns></returns>
        /// createdBy NHHAi 10/1/2022
        public ServiceResult DeleteAllService(List<string> entityIds);

        /// <summary>
        /// Hàm xuất excel
        /// </summary>
        /// <returns>stream</returns>
        /// createdBy NHHAi 19/1/2022
        public Stream ExportListUsingEPPlus();

        public ValidateResult DoValidate(T entity, Guid? entityId);
    }
}
