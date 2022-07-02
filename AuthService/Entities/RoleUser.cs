using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class RoleUser
    {
        public long ID { get; set; }
        public long RoleID { get; set; }
        public long UserID { get; set; }
    }
}
