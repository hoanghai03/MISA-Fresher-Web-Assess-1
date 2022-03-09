using MISA.Fresher.Core.Entities.PaymentDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// IPaymentDetailRepository
    /// createdBy NHHAi 20/2/2022
    /// </summary>
    public interface IPaymentDetailRepository : IBaseRepository<PaymentDetail>
    {
        // lấy payment theo id
        public IEnumerable<PaymentDetail> GetWithPaymentId(Guid paymentId);
    }
}
