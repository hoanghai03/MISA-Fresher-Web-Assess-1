using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities
{
    public class DataFilter
    {
        public int TotalPage{ get; set; }
        public int TotalRecord { get; set; }
        public IEnumerable<Employee> data{ get; set; }
    }
}
