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
        [ExportExcel]
        [PropertyName("Lý do thu/chi")]
        public string JournalMemo { get; set; }
        // nhân viên
        public Guid? EmployeeId { get; set; }
        // ngày hạch toán
        [ExportExcel]
        [PropertyName("Ngày hạch toán")]
        public DateTime? AccountingDate { get; set; }
        // phiếu chi
        [PropertyName("Ngày phiếu chi")]
        public DateTime? PaymentDate { get; set; }
        // số phiếu chi
        [NotDuplicate]
        [NotEmpty]
        [ExportExcel]
        [PropertyName("Số phiếu chi")]
        public string PaymentNumber { get; set; }
        // số chứng từ gốc
        public int? DocumentIncluded { get; set; }
        // tổng tiền
        [ExportExcel]
        [PropertyName("Số tiền")]
        public double? TotalAmount { get; set; }

        // diễn giải
        [ExportExcel]
        [PropertyName("Diễn giải")]
        public string Description { get; set; }
        // loại chứng từ
        [ExportExcel]
        [PropertyName("Loại chứng từ")]
        public string TypeDocument { get; set; }
        // hạch toán gộp nhiều hóa đơn
        [ExportExcel]
        [PropertyName("Hạch toán gộp nhiều hóa đơn")]
        public string AccountMultiple { get; set; }
        // chi nhanh
        [ExportExcel]
        [PropertyName("Chi nhánh")]
        public string Branch { get; set; }
    }
}
