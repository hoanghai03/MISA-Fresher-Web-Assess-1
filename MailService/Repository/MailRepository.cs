using Dapper;
using MailService.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MailService.Repository
{
    public class MailRepository : IMailRepository
    {
        protected string _connectionString = string.Empty;
        protected readonly IDbConnection _dbConnection;

        public MailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("connectDb");
            _dbConnection = new MySqlConnection(_connectionString);

        }

        public bool ConfirmEmail(string email)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                string sql = "UPDATE user u SET u.IsConfirmEmail = 1 WHERE u.Email = @Email and now() < ExpriedEmailConfirm";
                dynamicParameters.Add("@IsConfirmEmail", 1);
                dynamicParameters.Add("@Email", email);
                var res = _dbConnection.Execute(sql, param: dynamicParameters);
                if (res > 0) return true;
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
