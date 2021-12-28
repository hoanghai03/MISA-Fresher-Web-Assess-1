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
    public class Customer
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
    }
}
