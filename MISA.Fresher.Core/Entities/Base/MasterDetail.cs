using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Base
{
    /// <summary>
    /// MasterDetail
    /// </summary>
    /// <typeparam name="M">Entity</typeparam>
    /// <typeparam name="D">Entity</typeparam>
    /// createdBy NHHai 25/2/2022
    public class MasterDetail<M,D>
    {
        /// <summary>
        /// Entity
        /// </summary>
        public M Entity { get; set; }

        /// <summary>
        /// Enumerable
        /// </summary>
        public IEnumerable<D> EntityDetails { get; set; }
    }
}
