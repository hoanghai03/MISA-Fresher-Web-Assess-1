using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class ValidateField
    {
        /// <summary>
        /// field lỗi
        /// </summary>
        public string FieldError { get; set; }


        public int ErrorCode { get; set; }

        public string ErrorMessenge{ get; set; }
    }
}
