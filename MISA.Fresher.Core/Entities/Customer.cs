using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    /// <summary>
    /// Thông tin khách hàng
    /// createdBy NHHai 28/12/2021
    /// </summary>
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// createdBy NHHai 28/12/2021
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// createdBy NHHai 28/12/2021
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ tên
        /// createdBy NHHai 28/12/2021
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// createdBy NHHai 28/12/2021
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số điện thoại
        /// createdBy NHHai 28/12/2021
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// tên
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// tên cuối
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// giới tính
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// địa chỉ
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// email
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// mã customerGroup
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// số tiền ghi nợ
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public double? DebitAmount { get; set; }

        /// <summary>
        /// mã thẻ thành viên
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// tên công ty
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// mã số thuế công ty
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string CompanyTaxCode { get; set; }
    }
}
