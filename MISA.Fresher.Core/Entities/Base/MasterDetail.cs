using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Base
{
    public class MasterDetail<M,D>
    {
        public M Entity { get; set; }

        public IEnumerable<D> EntityDetails { get; set; }
    }
}
