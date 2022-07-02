using AuthService.Entities;
using AuthService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services
{
    public class AuthServicez : IAuthService
    {
        IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        public AuthServicez(IAuthRepository authRepository, IConfiguration config, IConfiguration b)
        {
            _authRepository = authRepository;
            _config = config;
        }
        public AuthResult Login(User user)
        {
            AuthResult authResult = new AuthResult();
            User checkLogin = _authRepository.Login(user);
            if (checkLogin != null)
            {
                authResult.Token = GenerateToken(checkLogin);
                authResult.RefreshToken = "hoanghai";

                // Save refresh token
                var saveToken = _authRepository.SaveRefreshToken(authResult.RefreshToken, checkLogin.ID);
            }
            else
            {
                return null;
            }
            return authResult;
        }



        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>();
            var secretKey = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]);
            var symmetricSecurityKey = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // add claims

            claims.Add(new Claim("UserId", user.ID.ToString()));
            claims.Add(new Claim("UserName", user.UserName));
            claims.Add(new Claim("CreatedAt", DateTime.Now.ToString()));

            var accessToken = new JwtSecurityToken(
                    issuer: _config["JwtSettings:Issuer"],
                    audience: _config["JwtSettings:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddSeconds(20000),
                    signingCredentials: credentials
                );

            return tokenHandler.WriteToken(accessToken);
        }

        public string GetToken(string refreshToken, Guid ID)
        {
            User user = _authRepository.GetUser(refreshToken, ID);

            if (user != null)
            {
                return this.GenerateToken(user);
            }
            return null;
        }

        public int DeleteRefreshToken(Guid ID)
        {
            var res = _authRepository.DeleteRefreshToken(ID);
            return res;
        }
    }
}
