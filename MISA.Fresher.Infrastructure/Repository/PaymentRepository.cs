using Microsoft.Extensions.Configuration;
using MISA.Fresher.Core.Entities.Payment;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Infrastructure.Repository
{
    public class PaymentRepository : BaseRepository<Payment>,IPaymentRepository
    {
        public PaymentRepository(IConfiguration configuration) : base(configuration) { }
    }
}
