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
using MISA.Fresher.Core.Entities.PaymentDetail;
using MISA.Fresher.Core.MISAAttribute;

namespace MISA.Fresher.Core.Services
{
    /// <summary>
    /// PaymentService
    /// createdBy NHHai 20/2/2022
    /// </summary>
    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        IPaymentRepository _paymentRepository;
        IPaymentDetailRepository _paymentDetailRepository;
        IPaymentDetailService _paymentDetailService;
        public PaymentService(IPaymentRepository paymentRepository, IPaymentDetailRepository paymentDetailRepository, IPaymentDetailService paymentDetailService) : base(paymentRepository,null)
        {
            _paymentRepository = paymentRepository;
            _paymentDetailRepository = paymentDetailRepository;
            _paymentDetailService = paymentDetailService;
    } 

        public ServiceResult GetNewPaymentNumber()
        {
            ServiceResult serviceResult = new ServiceResult();
            // chưa tối ưu ============================================//
            // lấy giá trị paymentNumber mới nhất 
            var paymentNumber = _paymentRepository.GetMaxCodeRepository();
            if (paymentNumber != null)
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
                while (_paymentRepository.CheckCodeDuplicate(paymentNumber) > 0)
                {
                    oldNumberString = newNumberString;
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
            }
            return serviceResult;
        }

        public ServiceResult InsertMasterDetail(Payment payment, IEnumerable<PaymentDetail> paymentDetails)
        {
            ServiceResult serviceResult = new ServiceResult();
            ValidateResult validateResult = DoValidate(payment, null);

            // nếu validate của payment lỗi
            if (!validateResult.IsValid)
            {
                // ghi lỗi
                serviceResult.Code = (int)Enum.Code.BadRequest;
                serviceResult.ValidateInfo = validateResult.ValidateInfo;
            }
            else
            {
                ValidateResult validateResultDetail = new ValidateResult();
                // lặp tất cả các giá trị trong paymentDetails
                foreach (PaymentDetail paymentDetail in paymentDetails)
                {
                    validateResultDetail = _paymentDetailService.DoValidate(paymentDetail, null);
                    // nếu validate lỗi thì dừng vòng lặp
                    if (!validateResultDetail.IsValid) break;
                }
                // ghi lỗi
                if (!validateResultDetail.IsValid)
                {

                    serviceResult.Code = (int)Enum.Code.BadRequest;
                    // gán validateinfo
                    serviceResult.ValidateInfo = validateResultDetail.ValidateInfo;

                }

                // gọi đến repository
                if (validateResult.IsValid)
                {
                    serviceResult.Data = _paymentRepository.InserMasterDetailRepo(payment, paymentDetails);
                }
            }
            

            return serviceResult;

        }

        public override bool ValidateCustom(Payment entity)
        {

            return true;
        }


        public ServiceResult GetMasterDetail(Guid paymentId)
        {
            ServiceResult serviceResult = new ServiceResult();
            MasterDetail<Payment, PaymentDetail> masterDetail = new MasterDetail<Payment, PaymentDetail>();
            // lấy master
            masterDetail.Entity = _paymentRepository.GetById(paymentId);
            // lấy details
            masterDetail.EntityDetails = _paymentDetailRepository.GetWithPaymentId(paymentId);
            serviceResult.Data = masterDetail;

            return serviceResult;
        }
    }
}
