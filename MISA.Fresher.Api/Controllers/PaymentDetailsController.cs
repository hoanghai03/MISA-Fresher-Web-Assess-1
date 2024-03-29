﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities.PaymentDetail;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    /// <summary>
    /// PaymentDetaisController
    /// createdBy NHHAI 20/2/2022
    /// </summary>
    public class PaymentDetailsController : BaseController<PaymentDetail>
    {
        public PaymentDetailsController(IBaseService<PaymentDetail> baseService) : base(baseService)
        {

        }
    }
}
