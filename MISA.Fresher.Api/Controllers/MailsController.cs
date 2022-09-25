using MailService.Entities;
using MailService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly ISendMailService _sendMailService;

        public MailsController(ISendMailService sendMailService)
        {
            this._sendMailService = sendMailService;
        }
        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody]string email)
        {
            try
            {
                MailContent mailContent = new MailContent()
                {
                    To = $"{email}",
                    Body = string.Format(@"
                    <p>Cảm ơn bạn đã đăng ký ứng dụng của chúng tôi, bạn hãy click vào link để xác nhận : </p>
                    <form method='post' action='http://localhost:5000/api/v1/mails/confirm?email={0}'>
                        <button type='submit'>Xác nhận</button>
                    </form>
                    ",email),
                    Subject = "Xác nhận Email"
                };
                await _sendMailService.SendMail(mailContent);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("confirm")]
        public IActionResult ConfirmEmail(string email)
        {
            try
            {
                var res = _sendMailService.ConfirmEmail(email);
                if (res) return Ok(res);
                return BadRequest(res);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
