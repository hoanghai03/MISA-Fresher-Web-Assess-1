using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Entities
{
    public class Action
    {
        public long ID{ get; set; }
        public string ActionName{ get; set; }
        public long ActionValue{ get; set; }
    }
}
