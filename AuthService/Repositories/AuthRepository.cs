using AuthService.Entities;
using AuthService.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AuthService.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        // Lấy thông tin database
        protected string _connectionString = string.Empty;

        public IDbConnection dbConnection;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("connectDB");
            dbConnection = new MySqlConnection(_connectionString);
        }

        public User Login(User user)
        {

            string sql = "select u.*,ul.RoleID as Role from user u join role_user ul on u.ID = ul.UserID where u.username = @username and u.password = @password and IsConfirmEmail = 1";
            User result = dbConnection.QuerySingleOrDefault<User>(sql, new { username = user.UserName, password = user.Password });

            return result;
        }

        public User GetUser(string refreshToken, Guid ID)
        {
            string sql = "select u.* from refresh_token join user u on u.ID = UserID where UserID = @UserID and RefreshTokenValue = @RefreshToken and NOW() < expriedDate";
            User result = dbConnection.QuerySingleOrDefault<User>(sql, new { UserID = ID, RefreshToken = refreshToken });

            return result;
        }


        public bool SaveRefreshToken(string refreshToken, Guid ID)
        {
            string sql = "INSERT INTO refresh_token (UserID, RefreshTokenValue, ExpriedDate) VALUES (@UserID, @RefreshTokenValue,  DATE_ADD(NOW(),INTERVAL 10 MINUTE))";
            int result = dbConnection.Execute(sql, new { UserID = ID, RefreshTokenValue = refreshToken });

            return result > 0;
        }        
        
        public int DeleteRefreshToken(Guid ID)
        {
            string sql = "Delete from refresh_token where UserID = @UserID";
            int result = dbConnection.Execute(sql, new { UserID = ID});

            return result;
        }

        public bool CheckExistUserName(string username)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            string sql = $"select username,password from user where username = @username";
            dynamicParameters.Add("@username", username);
            object res = dbConnection.QueryFirstOrDefault<object>(sql, param: dynamicParameters);
            if (res != null) return false;
            return true;
        }        
        
        public bool CheckExistEmail(string email)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            string sql = $"select username,password from user where email = @email";
            dynamicParameters.Add("@email", email);
            object res = dbConnection.QueryFirstOrDefault<object>(sql, param: dynamicParameters);
            if (res != null) return false;
            return true;
        }

        public User InserUser(Register user)
        {
            DynamicParameters dynamic = new DynamicParameters();
            try
            {
                dbConnection.Open();
                var transaction = dbConnection.BeginTransaction();

                Guid guid = Guid.NewGuid();

                //insert user
                string sql = "insert into user(ID,UserName,Password,Email,IsConfirmEmail,ExpriedEmailConfirm) values(@guid,@username,@password,@Email,0,DATE_ADD(NOW(),INTERVAL 10 MINUTE))";

                dynamic.Add("@guid", guid);
                dynamic.Add("@username", user.UserName);
                dynamic.Add("@password", user.Password);
                dynamic.Add("@Email", user.Email);
                var res = dbConnection.Execute(sql, param: dynamic, transaction);

                // insert role_user
                string sql2 = "insert into role_user(UserID,RoleID) values (@guid,2)";
                var res2 = dbConnection.Execute(sql2, param: dynamic, transaction);

                if (res == 0 || res2 ==0) return null;

                transaction.Commit();
                return new User() { UserName = user.UserName, Password = user.Password };
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }
    }
}
