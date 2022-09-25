using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class ValidateResult
    {
        public bool IsValid { get; set; } = true;

        public List<ValidateField> ListValidate { get; set; } = new List<ValidateField>();
    }
}
