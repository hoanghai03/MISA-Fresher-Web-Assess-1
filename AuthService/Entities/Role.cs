using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class Role
    {
        public long ID { get; set; }
        public string RoleName{ get; set; }
        public long RoleValue { get; set; }
    }
}
