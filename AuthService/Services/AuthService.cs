using AuthService.Attributee;
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
using System.Threading.Tasks;

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
                authResult.Role = checkLogin.Role;
                authResult.Id = checkLogin.ID;
                // Save refresh token
                var saveToken = _authRepository.SaveRefreshToken(authResult.RefreshToken, checkLogin.ID);
            }
            else
            {
                authResult.Message = "Đăng nhập không thành công";
                authResult.Success = false;
                return authResult;
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
                    expires: DateTime.Now.AddSeconds(10000),
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

        public async Task<int> DeleteRefreshToken(Guid ID)
        {
            //await Task.Delay(3000);
            var res = _authRepository.DeleteRefreshToken(ID);
            return res;
        }


        public ServiceResult RegisterService(Register account)
        {
            ServiceResult serviceResult = new ServiceResult();
            //validate
            var checkValidate = Validate(account);
            if (!checkValidate.IsValid)
            {
                serviceResult.Code = 400;
                serviceResult.ValidateInfo = checkValidate.ListValidate;
                serviceResult.ErrorMessage = "Dữ liệu lỗi cmnr :v";
            }
            else
            {
                serviceResult.Data = _authRepository.InserUser(account);
            }
            return serviceResult;
        }

        /// <summary>
        /// Hàm validate dữ liệu truyền vào
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ValidateResult Validate(Register entity)
        {
            ValidateResult validateResult = new ValidateResult();
            var properties = entity.GetType().GetProperties();
            // check dữ liệu trống
            foreach (var item in properties)
            {
                var notEmpty = item.GetCustomAttributes(typeof(NotEmpty), true);
                var value = item.GetValue(entity);
                if(notEmpty.Length > 0)
                {
                    if(value == null || string.IsNullOrEmpty(value.ToString().Trim()))
                    {
                        validateResult.IsValid = false;
                        validateResult.ListValidate.Add(new ValidateField() { FieldError = item.Name, ErrorCode = (int)Enum.Number.Number_1, ErrorMessenge = "Dữ liệu trống" });
                    }
                }
            }
            // check dữ liệu trùng
            if(validateResult.IsValid && entity.Password != entity.EnterThePassword)
            {
                validateResult.IsValid = false;
                validateResult.ListValidate.Add(new ValidateField() { ErrorCode = (int)Enum.Number.Number_2, ErrorMessenge = "Dữ liệu trùng password" });
            }
            // check dữ liệu đã có trong db hay chưa 
            if (validateResult.IsValid) { 
            // check trùng user
            var checkExistUsername = _authRepository.CheckExistUserName(entity.UserName);
            if(!checkExistUsername){
                validateResult.IsValid = false;
                    validateResult.ListValidate.Add(new ValidateField() { ErrorCode = (int)Enum.Number.Number_2, ErrorMessenge = "Dữ liệu trùng username" });
                }
            // check trùng email    
            var checkExistemail = _authRepository.CheckExistEmail(entity.Email);
            if(!checkExistemail)
                {
                validateResult.IsValid = false;
                    validateResult.ListValidate.Add(new ValidateField() { ErrorCode = (int)Enum.Number.Number_2, ErrorMessenge = "Dữ liệu trùng email" });
                }
            }
            return validateResult;
        }
    }
}
