using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Entities.Base
{
    public class ValidateResult
    {
        public bool IsValid { get; set; } = true;

        public List<ValidateField> ValidateInfo { get; set; } = new List<ValidateField>();
    }

    public class ValidateField
    {
        public string FieldName { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
