using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int Role{ get; set; }

        public string Email { get; set; }
        public bool IsConfirmEmail{ get; set; }

        public DateTime ExpriedEmailConfirm { get; set; }
    }
}
