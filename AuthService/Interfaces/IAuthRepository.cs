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


    }
}
