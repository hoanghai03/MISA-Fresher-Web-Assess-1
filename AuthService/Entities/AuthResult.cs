using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class AuthResult
    {
        public string Token{ get; set; }
        public string RefreshToken{ get; set; }
        public string Message{ get; set; }

        
    }
}
