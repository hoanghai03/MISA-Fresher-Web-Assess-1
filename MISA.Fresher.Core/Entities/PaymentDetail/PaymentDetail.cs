using MISA.Fresher.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.PaymentDetail
{
    public class PaymentDetail : BaseEntity
    {
        // khóa chính
        [Id]
        public Guid PaymentDetailId { get; set; }
        // khóa ngoại
        public Guid PaymentId { get; set; }
        // diễn giải
        public string Description { get; set; }
        // tài khoản nợ
        public Guid DebitAccount { get; set; }
        // tài khoản có
        public Guid CreditAccount { get; set; }
        // số tiền
        public double? Amount { get; set; }
        // mã code
        public string SupplierCode { get; set; }
        // tên đối tượng
        public string SupplierName { get; set; }
        // Mã nhà cung cấp
        public Guid? SupplierId { get; set; }
        // mã tài khoản nợ
        [Ignore]
        public string DebitAccountNumber{ get; set; }
        // mã tài khoản có
        [Ignore]
        public string CreditAccountNumber { get; set; }
    }
}
