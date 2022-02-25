using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities.Base;
using MISA.Fresher.Core.Entities.Payment;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    public class PaymentsController : BaseController<Payment>
    {
        IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService) : base(paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("NewPaymentNumber")]
        public IActionResult GetNewPaymentNumber()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult =  _paymentService.getNewPaymentNumber();
            }
            catch (Exception ex)
            {

                serviceResult.SetError(ex);
            }
            return Ok(serviceResult);

        }
    }
}
