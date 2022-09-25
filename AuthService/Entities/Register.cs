using AuthService.Attributee;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class Register
    {
        [NotEmpty]
        public string UserName { get; set; }
        [NotEmpty]
        public string Password { get; set; }
        [NotEmpty]
        public string EnterThePassword { get; set; }

        [NotEmpty]
        public string Email { get; set; }
    }
}
