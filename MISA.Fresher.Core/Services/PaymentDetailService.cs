﻿using MISA.Fresher.Core.Entities.PaymentDetail;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Services
{
    /// <summary>
    /// PaymentDetailService
    /// createdBy NHHAi 20/2/2022
    /// </summary>
    public class PaymentDetailService : BaseService<PaymentDetail> , IPaymentDetailService
    {
        public PaymentDetailService(IBaseRepository<PaymentDetail> baseRepository ) : base(baseRepository,null)
        {

        }
    }
}
