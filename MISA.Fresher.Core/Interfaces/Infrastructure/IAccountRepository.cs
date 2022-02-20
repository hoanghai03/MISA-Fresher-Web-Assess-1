using MISA.Fresher.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Infrastructure
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        int GenAccountTree();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Account> GetAccountTree();
    }
}
