using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Interfaces
{
    public interface IMailRepository
    {
        public bool ConfirmEmail(string email);
    }
}
