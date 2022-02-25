using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities.Account;
using MISA.Fresher.Core.Entities.Base;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Services
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        public IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository) : base(accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceResult GetAccountTree()
        {
            ServiceResult serviceResult = new ServiceResult();
            List<Account> accounts = new List<Account>();
            // lấy danh sách tài khoản
            accounts = _accountRepository.GetAccountTree();
            // lấy danh sách tài khoản cha
            List<Account> accountsTree = accounts.Where(account => account.ParentId == null).ToList();
            // khai báo child index
            int childIndex = 1;
            for(int i = 0; i < accountsTree.Count; i++)
            {
                accountsTree[i].ChildIndex = childIndex;
            }
            RecursiveAccount(accountsTree, accounts, childIndex);
            serviceResult.Data = accountsTree;
            return serviceResult;
        }

        /// <summary>
        /// Xử lý đệ quy để ghép account dạng tree
        /// </summary>
        /// <param name="list"></param>
        /// <param name="source"></param>
        public void RecursiveAccount(List<Account> list, List<Account> source,int childIndex)
        {
            childIndex++;
            for(int i = 0; i < list.Count; i++)
            {
                Guid parentId = list[i].AccountId;
                list[i].Children = source.Where(acount => acount.ParentId == parentId).ToList();
                for (int j = 0; j < list[i].Children.Count; j++)
                {
                    list[i].Children[j].ChildIndex = childIndex;
                }
                RecursiveAccount(list[i].Children, source,childIndex);
            }
        }

        public void GenAccountTree()
        {
            _accountRepository.GenAccountTree();
        }

        public ServiceResult GetAccountDebit()
        {
            return new ServiceResult() { Data = _accountRepository.GetAccountDebitRepository() };
        }

        public ServiceResult GetAccountCredit()
        {
            return new ServiceResult() { Data = _accountRepository.GetAccountCreditRepository() };

        }
    }
}
