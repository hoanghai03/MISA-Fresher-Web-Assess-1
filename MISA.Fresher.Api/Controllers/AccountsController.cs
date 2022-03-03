﻿using Microsoft.AspNetCore.Http;
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
    public class AccountsController : BaseController<Account>
    {
        public IAccountService _accountService;
        public AccountsController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("GetAccountTree")]
        public IActionResult GetAccountTree()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _accountService.GetAccountTree();
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);
            }

            return Ok(serviceResult);
        }

        [HttpGet("GenAccountTree")]
        public IActionResult GenAccountTree()
        {
            try
            {
                _accountService.GenAccountTree();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpGet("GetAccountDebit")]
        public IActionResult GetAccountDebit()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _accountService.GetAccountDebit();
            }
            catch(Exception ex)
            {
                serviceResult.SetError(ex);
            }

            return Ok(serviceResult);
        }

        [HttpGet("GetAccountCredit")]
        public IActionResult GetAccountCredit()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _accountService.GetAccountCredit();
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);
            }

            return Ok(serviceResult);
        }


        [HttpGet("AccountId/{accountId}")]
        public IActionResult DeleteAccountId(Guid accountId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
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
