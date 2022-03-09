using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities.Account;
using MISA.Fresher.Core.Entities.Base;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// createdBy NHHai 20/2/2022
    /// </summary>
    public class AccountsController : BaseController<Account>
    {
        public IAccountService _accountService;
        public AccountsController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Hàm lấy tài khoản theo dạng cây
        /// </summary>
        /// <returns>trả về accounts</returns>
        /// createdBy NHHai 20/2/2022
        [HttpGet("GetAccountTree")]
        public IActionResult GetAccountTree()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                // gọi đến service
                serviceResult = _accountService.GetAccountTree();
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);
            }

            return Ok(serviceResult);
        }


        /// <summary>
        /// Hàm sinh dữ liệu tài khoản
        /// </summary>
        /// <returns>trả về accounts</returns>
        /// createdBy NHHai 20/2/2022
        [HttpGet("GenAccountTree")]
        public IActionResult GenAccountTree()
        {
            try
            {
                // gọi đến service
                _accountService.GenAccountTree();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }


        /// <summary>
        /// Hàm lấy tài khoản nợ
        /// </summary>
        /// <returns>trả về accounts</returns>
        /// createdBy NHHai 20/2/2022
        [HttpGet("GetAccountDebit")]
        public IActionResult GetAccountDebit()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                // gọi đến service
                serviceResult = _accountService.GetAccountDebit();
            }
            catch(Exception ex)
            {
                serviceResult.SetError(ex);
            }

            return Ok(serviceResult);
        }


        /// <summary>
        /// Hàm lấy tài khoản có
        /// </summary>
        /// <returns>trả về accounts</returns>
        /// createdBy NHHai 20/2/2022
        [HttpGet("GetAccountCredit")]
        public IActionResult GetAccountCredit()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                // gọi đến service
                serviceResult = _accountService.GetAccountCredit();
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);
            }

            return Ok(serviceResult);
        }


        /// <summary>
        /// Hàm lấy tài khoản theo accountId
        /// </summary>
        /// <returns>trả về accounts</returns>
        /// createdBy NHHai 20/2/2022
        [HttpGet("AccountId/{accountId}")]
        public IActionResult DeleteAccountId(Guid accountId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                // gọi đến service
                serviceResult = _accountService.GetAccountWithChild(accountId);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);
            }

            return Ok(serviceResult);
        }

    }
}
