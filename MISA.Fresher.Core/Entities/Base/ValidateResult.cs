using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Base
{
    /// <summary>
    /// ValidateResult
    /// createdBy NHHAi 25/2/2022
    /// </summary>
    public class ValidateResult
    {
        // có lỗi hay không (true-có lỗi,false-không có lỗi) 
        public bool IsValid { get; set; } = true;
        // danh sách lỗi
        public List<ValidateField> ValidateInfo { get; set; } = new List<ValidateField>();
    }

    /// <summary>
    /// ValidateField
    /// createdBy NHHAi 25/2/2022
    /// </summary>
    public class ValidateField
    {
        // tên property
        public string FieldName { get; set; }
        // mã lỗi
        public int ErrorCode { get; set; }
        // thông tin lỗi
        public string ErrorMessage { get; set; }
    }
}
