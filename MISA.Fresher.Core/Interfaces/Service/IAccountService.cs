using MISA.Fresher.Core.Entities.Account;
using MISA.Fresher.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    public interface IAccountService : IBaseService<Account>
    {
        void GenAccountTree();
        ServiceResult GetAccountTree();
    }
}
