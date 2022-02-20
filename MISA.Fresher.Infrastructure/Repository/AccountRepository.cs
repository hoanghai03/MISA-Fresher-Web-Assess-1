using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities.Account;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IConfiguration IConfiguration) : base(IConfiguration)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccountTree()
        {
            using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
            {
                string storeName = "Proc_GetAccountTree";
                return mySqlConnector.Query<Account>(storeName, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }


        public List<Account> GenAccountRoot()
        {
            var result = new List<Account>();
            for (int i = 1; i <= 50; i++)
            {
                string accountNumber = "00" + i;
                Guid accountId = Guid.NewGuid();

                string sqlCommand = $"INSERT INTO Account (AccountId, AccountNumber, AccountName, AccountNameEnglish, AccountCategoryKind, Description, ForeignCurrencyAccount, DetailByObjectKind, DetailByCostAggregationObjKind, DetailByOrderKind, DetailByPurchaseContractKind, DetailByUnitKind, DetailByAccount, DetailByContructionKind, DetailByContractSaleKind, DetailByExpenseItemKind, DetailByStatisticalCodeKind, DetailByObject, DetailByCostAggregationObj, DetailByOrder, DetailByPurchaseContract, DetailByUnit, DetailByContruction, DetailByContractSale, DetailByExpenseItem, DetailByStatisticalCode, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ParentId) " +
                    $"VALUES('{accountId.ToString()}', '{accountNumber}', 'Test{accountNumber}', '', 1, '', 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, '', NOW(), '', NOW(), NULL);";

                result.Add(new Account() { AccountId = accountId, AccountNumber = accountNumber });
                using (var db = new MySqlConnection(_connectionString))
                {
                    db.Execute(sqlCommand);
                }
            }

            return result;
        }

        public int GenAccountTree()
        {
            var parentSource = GenAccountRoot();
            for (int i = 0; i < parentSource.Count; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    string accountNumber = parentSource[i].AccountNumber + j * 10 + new Random().Next(1, 100);
                    Guid parentId = parentSource[i].AccountId;
                    Guid accountId = Guid.NewGuid();

                    var account = new Account() { AccountId = accountId, ParentId = parentId, AccountNumber = accountNumber };
                    string sqlCommand = $"INSERT INTO Account (AccountId, AccountNumber, AccountName, AccountNameEnglish, AccountCategoryKind, Description, ForeignCurrencyAccount, DetailByObjectKind, DetailByCostAggregationObjKind, DetailByOrderKind, DetailByPurchaseContractKind, DetailByUnitKind, DetailByAccount, DetailByContructionKind, DetailByContractSaleKind, DetailByExpenseItemKind, DetailByStatisticalCodeKind, DetailByObject, DetailByCostAggregationObj, DetailByOrder, DetailByPurchaseContract, DetailByUnit, DetailByContruction, DetailByContractSale, DetailByExpenseItem, DetailByStatisticalCode, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ParentId) " +
                    $"VALUES('{accountId.ToString()}', '{accountNumber}', 'Test{accountNumber}', '', 1, '', 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, '', NOW(), '', NOW(), '{parentId.ToString()}');";
                    using (var db = new MySqlConnection(_connectionString))
                    {
                        db.Execute(sqlCommand);
                    }

                    for (int k = 1; k <= 2; k++)
                    {
                        string accountNumber2 = account.AccountNumber + k * 10 + new Random().Next(1, 100);
                        string parentId2 = account.AccountId.ToString();
                        string accountId2 = Guid.NewGuid().ToString();

                        string sqlCommand2 = $"INSERT INTO Account (AccountId, AccountNumber, AccountName, AccountNameEnglish, AccountCategoryKind, Description, ForeignCurrencyAccount, DetailByObjectKind, DetailByCostAggregationObjKind, DetailByOrderKind, DetailByPurchaseContractKind, DetailByUnitKind, DetailByAccount, DetailByContructionKind, DetailByContractSaleKind, DetailByExpenseItemKind, DetailByStatisticalCodeKind, DetailByObject, DetailByCostAggregationObj, DetailByOrder, DetailByPurchaseContract, DetailByUnit, DetailByContruction, DetailByContractSale, DetailByExpenseItem, DetailByStatisticalCode, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ParentId) " +
                        $"VALUES('{accountId2}', '{accountNumber2}', 'Test{accountNumber2}', '', 1, '', 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, '', NOW(), '', NOW(), '{parentId2}');";
                        using (var db = new MySqlConnection(_connectionString))
                        {
                            db.Execute(sqlCommand2);
                        }
                    }
                }
            }

            return 0;
        }
    }
}
