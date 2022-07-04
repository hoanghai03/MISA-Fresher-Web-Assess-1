using AuthService.Caching;
using AuthService.Entities;
using AuthService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MISA.Fresher.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        private readonly IHttpContextAccessor accessor;

        public AuthController(IAuthService authService, IHttpContextAccessor accessor)
        {
            _authService = authService;
            this.accessor = accessor;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            try
            {
                var res = _authService.Login(user);
                if (res != null)
                {
                    return Ok(res);
                }
                return BadRequest(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get-token")]
        public IActionResult GetToken(string refreshToken, Guid ID)
        {
            try
            {
                var res = _authService.GetToken(refreshToken, ID);
                if (res != null)
                {
                    return Ok(res);
                }
                return BadRequest("Không lấy được token");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("logout")]
        [Authorize]
        public IActionResult Logout(Guid userID)
        {
            try
            {
                var bearerToken = accessor.HttpContext.Request.Headers[HeaderNames.Authorization];

                MemoryCacheHelper.Set($"{bearerToken}", 1, 20);
                var res = _authService.DeleteRefreshToken(userID);
                if (res!=0)
                {
                    return Ok(res);
                }
                return BadRequest("Không xóa được refresh-token");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpGet("get-data")]
        public IActionResult GetData()
        {
            //var bearerToken = accessor.HttpContext.Request.Headers[HeaderNames.Authorization];

            //var inBlackList = MemoryCacheHelper.Get(bearerToken);
            //if(inBlackList != null)
            //{
            //    return Unauthorized();
            //}

            return Ok("Data");
        }
    }
}
