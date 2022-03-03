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
        [PropertyName("Mã paymentId")]
        public Guid PaymentId { get; set; }
        // diễn giải
        public string Description { get; set; }
        // tài khoản nợ
        [NotEmpty]
        [PropertyName("Mã DebitAccount")]
        public Guid DebitAccount { get; set; }
        // tài khoản có
        [NotEmpty]
        [PropertyName("Mã CreditAccount")]
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
        public string DebitAccountNumber{ get; set; }
        // mã tài khoản có
        public string CreditAccountNumber { get; set; }
    }
}
