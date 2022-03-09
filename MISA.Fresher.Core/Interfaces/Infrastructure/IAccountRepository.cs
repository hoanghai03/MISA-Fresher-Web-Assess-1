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
        /// Lấy tài khoản theo dạng cây
        /// </summary>
        /// <returns></returns>
        /// createdBy NHHAi 20/2/2022
        List<Account> GetAccountTree();

        /// <summary>
        /// Hàm đếm lấy tài khoản nợ
        /// </summary>
        /// <returns></returns>
        /// createdBy NHHai 2/3/2022
        IEnumerable<Account> GetAccountDebitRepository();
        /// <summary>
        /// Hàm đếm số tài khoản có
        /// </summary>
        /// <returns></returns>
        /// createdBy NHHai 2/3/2022
        IEnumerable<Account> GetAccountCreditRepository();

        /// <summary>
        /// Hàm đếm số lượng bản ghi với parentId
        /// </summary>
        /// <returns></returns>
        /// createdBy NHHai 2/3/2022
        int CountAccountByParentId(Guid accountId);

        /// <summary>
        /// Hàm đếm số lượng bản ghi
        /// </summary>
        /// <returns></returns>
        /// createdBy NHHai 2/3/2022
        public int CountAccounts();
    }
}
