using MISA.Fresher.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    /// <summary>
    /// Thông tin nhà cung cấp
    /// createdBy NHHai 13/12/2021
    /// </summary>
    public class Supplier
    {
        // khóa chính
        [Id]
        public Guid SupplierId { get; set; } = Guid.Empty;
        // Mã số thuế
        [PropertyName("Mã số thuế")]
        [ExportExcel]
        public string SupplierTaxCode { get; set; }
        // Mã nhà cung cấp
        [PropertyName("Mã nhà cung cấp")]
        [NotEmpty]
        [NotDuplicate]
        [ExportExcel]
        public string SupplierCode { get; set; }
        // Tên nhà cung cấp
        [PropertyName("Tên nhà cung cấp")]
        [NotEmpty]
        [ExportExcel]
        public string SupplierName { get; set; }
        // Địa chỉ
        [PropertyName("Địa chỉ")]
        [ExportExcel]
        public string Address { get; set; }
        // Số điện thoại
        [PropertyName("Điện thoại")]
        [ExportExcel]
        public string PhoneNumber { get; set; }
        // Website
        public string Website { get; set; }
        // Mã nhóm nhà cung cấp
        [PropertyName("Nhóm KH,NCC")]
        [ExportExcel]
        public string SupplierGroupIds { get; set; }
        // ID nhân viên (khóa ngoại)
        public Guid? EmployeeId { get; set; }

        // Họ tên người liên hệ
        public string ContactName { get; set; }
        // Email liên hệ
        public string ContactEmail { get; set; }
        // Số điện thoại liên hệ
        public string ContactPhoneNumber { get; set; }
        // Nợ
        public string Debt { get; set; }
        // Đại diện theo pháp luật
        public string LegalRepresentative { get; set; }
        // họ và tên
        [Ignore]
        public string FullName{ get; set; }

        // diễn giải
        public string Description { get; set; }

        // Xưng hô
        public int? Prefix { get; set; }
        // giới tính
        [Ignore]
        [PropertyName("Danh xưng")]
        public string PrefixName
        {
            get
            {
                switch (Prefix)
                {
                    case (int) Enum.Prefix.Grandfather:
                        return Properties.Resources.Enum_Prefix_Grandfather;
                    case (int) Enum.Prefix.Grandma:
                        return Properties.Resources.Enum_Prefix_Grandma;
                    case (int)Enum.Prefix.You:
                        return Properties.Resources.Enum_Prefix_You;
                    case (int)Enum.Prefix.Sister:
                        return Properties.Resources.Enum_Prefix_Sister;
                    case (int)Enum.Prefix.Mr:
                        return Properties.Resources.Enum_Prefix_Mr;
                    case (int)Enum.Prefix.Mrs:
                        return Properties.Resources.Enum_Prefix_Mrs;
                    case (int)Enum.Prefix.Brother:
                        return Properties.Resources.Enum_Prefix_Brother;
                    case (int)Enum.Prefix.Ms:
                        return Properties.Resources.Enum_Prefix_Ms;
                    case (int)Enum.Prefix.Uncle:
                        return Properties.Resources.Enum_Prefix_Uncle;
  
                    default:
                        return null;
                }
            }
        }

    }
}
