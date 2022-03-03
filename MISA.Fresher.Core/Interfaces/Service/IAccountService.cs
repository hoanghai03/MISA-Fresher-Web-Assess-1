using MISA.Fresher.Core.Entities.Account;
using MISA.Fresher.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    /// <summary>
    /// interface accountService
    /// createdBy NHHAi 23/2/2022
    /// </summary>
    public interface IAccountService : IBaseService<Account>
    {

        /// <summary>
        /// Sinh dữ liệu
        /// createdBy NHHAi 23/2/2022
        /// </summary>
        void GenAccountTree();

        /// <summary>
        /// Lấy dữ liệu dạng cây
        /// </summary>
        /// <returns>Trả về ServiceResult</returns>
        /// createdBy NHHAI 23/2/2022
        public ServiceResult GetAccountTree();

        /// <summary>
        /// Lấy taif khoản nợ
        /// </summary>
        /// <returns>Trả về ServiceResult</returns>
        /// createdBy NHHAI 23/2/2022
        public ServiceResult GetAccountDebit();

        /// <summary>
        /// Lấy tài khoản có
        /// </summary>
        /// <returns>Trả về ServiceResult</returns>
        /// createdBy NHHAI 23/2/2022
        public ServiceResult GetAccountCredit();

        /// <summary>
        /// Lấy tài khoản theo id
        /// </summary>
        /// <returns>Trả về ServiceResult</returns>
        /// createdBy NHHAI 23/2/2022
        public ServiceResult GetAccountWithChild(Guid AccountId);
    }                                    
}
