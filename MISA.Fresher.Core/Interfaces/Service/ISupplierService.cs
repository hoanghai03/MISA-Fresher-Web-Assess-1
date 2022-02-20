using MISA.Fresher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    /// <summary>
    /// interface supplier service
    /// createdBy NHHAi 13/12/2022
    /// </summary>
    public interface ISupplierService:IBaseService<Supplier>
    {
        /// <summary>
        /// Hàm gọi đến repository để lấy mã nhà cung cấp mới
        /// </summary>
        /// <returns>Trả về mã ncc mới</returns>
        /// createdBy NHHAi 2/1/2022
        public string GetNewSupplierCode();
    }
}
