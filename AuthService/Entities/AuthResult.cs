using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class AuthResult
    {
        public string Token{ get; set; }
        public string RefreshToken{ get; set; }

        public Guid Id { get; set; }
        public string Message{ get; set; }
        public bool Success { get; set; } = true;
        public int Role { get; set; }
    }
}
