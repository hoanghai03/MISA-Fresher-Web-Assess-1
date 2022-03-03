using MISA.Fresher.Core.Entities.Payment;
using MISA.Fresher.Core.Entities.PaymentDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Infrastructure
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        /// <summary>
        /// Hàm lưu master detail
        /// </summary>
        /// <returns></returns>
        /// createdBy NHHAi 26/2/2022
        public int InserMasterDetailRepo(Payment payment , IEnumerable<PaymentDetail> paymentDetails);
    }
}
