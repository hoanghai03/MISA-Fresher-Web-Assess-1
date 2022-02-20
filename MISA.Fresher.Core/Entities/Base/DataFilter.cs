using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    /// <summary>
    /// data filter
    /// createdBy NHHAi 19/1/2022
    /// </summary>
    public class DataFilter<T>
    {
        // tổng số trang
        public int TotalPage{ get; set; }
        // tổng số bản ghi
        public int TotalRecord { get; set; }
        // data
        public IEnumerable<T> data{ get; set; }
    }
}
