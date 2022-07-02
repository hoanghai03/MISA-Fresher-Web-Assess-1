using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class RefreshToken
    {
        public long ID { get; set; }
        public Guid UserID { get; set; }
        public string RefreshTokenValue{ get; set; }
        public DateTime ExpriedDate{ get; set; }
    }
}
