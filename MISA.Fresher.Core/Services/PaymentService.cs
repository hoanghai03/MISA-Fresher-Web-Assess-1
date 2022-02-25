using MISA.Fresher.Core.Entities.Payment;
using MISA.Fresher.Core.Interfaces.Service;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MISA.Fresher.Core.Entities.Base;

namespace MISA.Fresher.Core.Services
{
    public class PaymentService : BaseService<Payment>,IPaymentService
    {
        IBaseRepository<Payment> _base;
        public PaymentService(IPaymentRepository paymentRepository) : base(paymentRepository)
        {
            _base = paymentRepository;
        }

        public ServiceResult getNewPaymentNumber()
        {
            ServiceResult serviceResult = new ServiceResult();
            // chưa tối ưu ============================================//
            // lấy giá trị paymentNumber mới nhất 
            var paymentNumber = _base.GetMaxCodeRepository();
            if (paymentNumber!=null)
            {
                // lấy number từ paymentNumber
                string[] numbers = Regex.Split(paymentNumber, @"\D+");
                var i = 0;
                // xét trường hợp nếu number[0] == ""
                if (numbers[0] == "") i = 1;
                var oldNumber = int.Parse(numbers[i]);
                string oldNumberString = oldNumber.ToString();

                var newNumber = int.Parse(numbers[i]) + 1;
                string newNumberString = newNumber.ToString();
                paymentNumber = paymentNumber.Replace(oldNumberString, newNumberString);

                // kiếm tra mã mới đã có trong db hay chưa 
                int j = 2;
                while (_base.CheckCodeDuplicate(paymentNumber) > 0)
                {
                    newNumber = int.Parse(numbers[i]) + j;
                    newNumberString = newNumber.ToString();
                    paymentNumber = paymentNumber.Replace(oldNumberString, newNumberString);
                    j++;
                }
                // gán dữ liệu cho serviceResult
                serviceResult.Data = paymentNumber;
            }
            else
            {
                serviceResult.Code = 500;
                serviceResult.ErrorMessage = Properties.Resources.Null;
                serviceResult.Success = false;
            }
            return serviceResult;
        }

        public override bool ValidateCustom(Payment entity)
        {

            return true;
        }
    }
}
