using MISA.Fresher.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    /// <summary>
    /// phòng ban
    /// createdBy NHHai 1/1/2021
    /// </summary>
    public class Department : BaseEntity
    {
        /// <summary>
        /// id
        /// createdBy NHHAi 1/1/2021
        /// </summary>
        [NotEmpty]
        [PropertyName("DepartmentId")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// code
        /// createdBy NHHAi 1/1/2021
        /// </summary>
        [NotEmpty]
        [PropertyName("mã phòng ban")]
        [NotDuplicate]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// tên phòng ban
        /// createdBy NHHAi 1/1/2021
        /// </summary>
        [NotEmpty]
        [PropertyName("tên phòng ban")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// mô tả
        /// createdBy NHHAi 1/1/2021
        /// </summary>
        public string Description { get; set; }

    }
}
