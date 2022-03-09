using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Base
{
    /// <summary>
    /// EntityTree
    /// createdBy NHHAi 25/2/2022
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityTree<T>
    {
        // danh sách entity
        public List<T> Entitys { get; set; }

        // tổng số bản ghi
        public int TotalEntitys { get; set; }

    }
}
