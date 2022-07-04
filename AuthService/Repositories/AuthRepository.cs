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

            string sql = "select u.*,ul.RoleID as Role from user u join role_user ul on u.ID = ul.UserID where u.username = @username and u.password = @password";
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
    }
}
