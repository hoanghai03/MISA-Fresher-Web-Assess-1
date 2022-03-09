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
    /// <summary>
    /// AccountRepository
    /// createdBy NHHai 20/2/2022
    /// </summary>
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IConfiguration IConfiguration) : base(IConfiguration)
        {

        }

        public List<Account> GetAccountTree()
        {
            using (MySqlConnection mySqlConnector = new MySqlConnection(_connectionString))
            {
                string storeName = "Proc_GetAccountTree";
                // gọi đến store
                return mySqlConnector.Query<Account>(storeName, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }


        public List<Account> GenAccountRoot()
        {
            var result = new List<Account>();
            for (int i = 1; i <= 20; i++)
            {
                string accountNumber = "00" + i;
                Guid accountId = Guid.NewGuid();

                string sqlCommand = $"INSERT INTO Account (AccountId, AccountNumber, AccountName, AccountNameEnglish, AccountCategoryKind, Description, ForeignCurrencyAccount, DetailByObjectKind, DetailByCostAggregationObjKind, DetailByOrderKind, DetailByPurchaseContractKind, DetailByUnitKind, DetailByAccount, DetailByContructionKind, DetailByContractSaleKind, DetailByExpenseItemKind, DetailByStatisticalCodeKind, DetailByObject, DetailByCostAggregationObj, DetailByOrder, DetailByPurchaseContract, DetailByUnit, DetailByContruction, DetailByContractSale, DetailByExpenseItem, DetailByStatisticalCode, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ParentId,Status) " +
                    $"VALUES('{accountId.ToString()}', '{accountNumber}', 'Tiền gửi ngân hàng', 'Bank deposits', 1, 'Tài khoản', 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, '', NOW(), '', NOW(), NULL,1);";

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
                for (int j = 1; j <= 10; j++)
                {
                    string accountNumber = parentSource[i].AccountNumber + j * 10 + new Random().Next(1, 100);
                    Guid parentId = parentSource[i].AccountId;
                    Guid accountId = Guid.NewGuid();

                    var account = new Account() { AccountId = accountId, ParentId = parentId, AccountNumber = accountNumber };
                    string sqlCommand = $"INSERT INTO Account (AccountId, AccountNumber, AccountName, AccountNameEnglish, AccountCategoryKind, Description, ForeignCurrencyAccount, DetailByObjectKind, DetailByCostAggregationObjKind, DetailByOrderKind, DetailByPurchaseContractKind, DetailByUnitKind, DetailByAccount, DetailByContructionKind, DetailByContractSaleKind, DetailByExpenseItemKind, DetailByStatisticalCodeKind, DetailByObject, DetailByCostAggregationObj, DetailByOrder, DetailByPurchaseContract, DetailByUnit, DetailByContruction, DetailByContractSale, DetailByExpenseItem, DetailByStatisticalCode, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ParentId,Status) " +
                    $"VALUES('{accountId.ToString()}', '{accountNumber}', 'Ngoại tệ', 'Foreign Currency', 1, 'Tài khoản', 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, '', NOW(), '', NOW(), '{parentId.ToString()}',1);";
                    using (var db = new MySqlConnection(_connectionString))
                    {
                        db.Execute(sqlCommand);
                    }

                    for (int k = 1; k <= 2; k++)
                    {
                        string accountNumber2 = account.AccountNumber + k * 10 + new Random().Next(1, 100);
                        string parentId2 = account.AccountId.ToString();
                        string accountId2 = Guid.NewGuid().ToString();

                        string sqlCommand2 = $"INSERT INTO Account (AccountId, AccountNumber, AccountName, AccountNameEnglish, AccountCategoryKind, Description, ForeignCurrencyAccount, DetailByObjectKind, DetailByCostAggregationObjKind, DetailByOrderKind, DetailByPurchaseContractKind, DetailByUnitKind, DetailByAccount, DetailByContructionKind, DetailByContractSaleKind, DetailByExpenseItemKind, DetailByStatisticalCodeKind, DetailByObject, DetailByCostAggregationObj, DetailByOrder, DetailByPurchaseContract, DetailByUnit, DetailByContruction, DetailByContractSale, DetailByExpenseItem, DetailByStatisticalCode, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ParentId,Status) " +
                        $"VALUES('{accountId2}', '{accountNumber2}', 'Cổ phiếu', 'Stock', 1, 'Tài khoản', 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, '', NOW(), '', NOW(), '{parentId2}',1);";
                        using (var db = new MySqlConnection(_connectionString))
                        {
                            db.Execute(sqlCommand2);
                        }
                    }
                }
            }

            return 0;
        }

        public IEnumerable<Account> GetAccountDebitRepository()
        {
            using(MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    // gọi đến store
                    var response = mySqlConnection.Query<Account>("Proc_GetAccountDebit", commandType: System.Data.CommandType.StoredProcedure);
                   return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<Account> GetAccountCreditRepository()
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    // gọi đến store
                    var response = mySqlConnection.Query<Account>("Proc_GetAccountCredit", commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int CountAccountByParentId(Guid accountId)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@ParentId", accountId);
                    // gọi đến store
                    int response = mySqlConnection.QueryFirstOrDefault<int>("Proc_CountAccountByParentId",param: parameter, commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }  
        public int CountAccounts()
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                try
                {
                    // gọi đến store
                    int response = mySqlConnection.QueryFirstOrDefault<int>("Proc_CountAccounts", commandType: System.Data.CommandType.StoredProcedure);
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
