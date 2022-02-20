using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    /// <summary>
    /// class thuộc tính chung của entity
    /// createdBy NHHAi 1/1/2021
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Thông tin ngày tạo dữ liệu
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Thông tin người tạo dữ liệu
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Thông tin ngày sửa dữ liệu
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Thông tin người sửa dữ liệu
        /// createdBy NHHAi 1/1/2022
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
