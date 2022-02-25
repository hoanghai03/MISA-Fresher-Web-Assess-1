using MISA.Fresher.Core.Entities.Base;
using MISA.Fresher.Core.Entities.Payment;
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
        public ServiceResult getNewPaymentNumber();
    }
}
