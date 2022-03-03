using MISA.Fresher.Core.Entities.Base;
using MISA.Fresher.Core.Entities.Payment;
using MISA.Fresher.Core.Entities.PaymentDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Interfaces.Service
{
    /// <summary>
    /// interface paymentService
    /// createdBy NHHAi 23/2/2022
    /// </summary>
    public interface IPaymentService : IBaseService<Payment>
    {
        /// <summary>
        /// Hàm lấy mã paymentNumber mới
        /// </summary>
        /// createdBy NHHAI 23/2/2022
        /// <returns>string</returns>
        public ServiceResult GetNewPaymentNumber();  
        
        
        /// <summary>
        /// Hàm insert master detail
        /// </summary>
        /// createdBy NHHAI 23/2/2022
        /// <returns>string</returns>
        public ServiceResult InsertMasterDetail(Payment payment,IEnumerable<PaymentDetail> paymentDetails);
        
        
        /// <summary>
        /// Hàm get master detail
        /// </summary>
        /// createdBy NHHAI 23/2/2022
        /// <returns>string</returns>
        public ServiceResult GetMasterDetail(Guid paymentId);

        


    }
}
