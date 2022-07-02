using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Interfaces
{
    public interface IAuthService
    {
        public AuthResult Login(User user);

        public string GetToken(string refreshToken,Guid ID);
        public int DeleteRefreshToken(Guid ID);


    }
}
