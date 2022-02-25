using MISA.Fresher.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Payment
{
    public class Payment : BaseEntity
    {
        // khóa chính
        [Id]
        public Guid PaymentId { get; set; }
        // khóa ngoại ( nhà cung cấp) 
        public Guid? SupplierId { get; set; }
        // đối tượng
        public string SupplierObject { get; set; }
        // người nhận
        public string SupplierContactName { get; set; }
        // địa chỉ
        public string Address { get; set; }
        // lý do chi
        public string JournalMemo { get; set; }
        // nhân viên
        public Guid? EmployeeId { get; set; }
        // ngày hạch toán
        [CheckDate]
        [PropertyName("Ngày hạch toán")]
        public DateTime? AccountingDate { get; set; }
        // phiếu chi
        [CheckDate]
        [PropertyName("Ngày phiếu chi")]
        public DateTime? PaymentDate { get; set; }
        // số phiếu chi
        [NotDuplicate]
        [NotEmpty]
        [PropertyName("Số phiếu chi")]
        public string PaymentNumber { get; set; }
        // số chứng từ gốc
        public int? DocumentIncluded { get; set; }
        // tổng tiền
        public double? TotalAmount { get; set; }
    }
}
