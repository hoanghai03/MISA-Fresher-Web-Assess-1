using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Interfaces
{
    public interface IAuthRepository
    {
        public User Login(User user);
        public User GetUser(string refreshToken,Guid ID);
        public bool SaveRefreshToken(string refreshToken,Guid ID);
        public int DeleteRefreshToken(Guid ID);

        /// <summary>
        /// Kiểm tra xem userName đã tồn tại trong db hay chưa
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        public bool CheckExistUserName(string username);
        public bool CheckExistEmail(string username);

        public User InserUser(Register user);


    }
}
