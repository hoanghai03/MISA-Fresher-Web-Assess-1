using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    /// <summary>
    /// Nhóm nhà cung cấp
    /// NHHAI 15/2/2022
    /// </summary>
    public class SupplierGroup : BaseEntity
    {
        // khóa chính
        public Guid SupplierGroupId { get; set; }
        // mã nhóm nhà cung cấp
        public string SupplierGroupCode { get; set; }
        // tên nhóm nhà cung cấp
        public string SupplierGroupName { get; set; }
    }
}
