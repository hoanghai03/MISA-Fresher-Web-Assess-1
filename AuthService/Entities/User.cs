﻿using System;
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
    }
}
