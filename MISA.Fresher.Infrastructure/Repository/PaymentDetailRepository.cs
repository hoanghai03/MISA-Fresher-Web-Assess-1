using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities.PaymentDetail;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    public class PaymentDetailRepository : BaseRepository<PaymentDetail>,IPaymentDetailRepository
    {
        public PaymentDetailRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
