using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// createdBy NHHai 28/12/2021
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Khóa chính
        /// createdBy NHHai 28/12/2021
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// createdBy NHHai 28/12/2021
        /// </summary>
        public string EmployeeCode { get; set; }

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

    }
}
