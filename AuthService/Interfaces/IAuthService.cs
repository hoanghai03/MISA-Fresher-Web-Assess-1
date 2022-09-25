using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Interfaces
{
    public interface IAuthService
    {
        public AuthResult Login(User user);

        public string GetToken(string refreshToken,Guid ID);
        public Task<int> DeleteRefreshToken(Guid ID);

        /// <summary>
        /// Hàm đăng ký
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ServiceResult RegisterService(Register account);


    }
}
