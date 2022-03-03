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
    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        IPaymentRepository _paymentRepository;
        IPaymentDetailRepository _paymentDetailRepository;
        IPaymentDetailService _paymentDetailService;
        public PaymentService(IPaymentRepository paymentRepository, IPaymentDetailRepository paymentDetailRepository, IPaymentDetailService paymentDetailService) : base(paymentRepository)
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
                serviceResult.Success = false;
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
                serviceResult.Success = false;
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
                    serviceResult.Success = false;
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

        //public ValidateResult DoValidateDetail(PaymentDetail entity, Guid? entityId)
        //{
        //    ValidateResult result = new ValidateResult();
        //    List<string> errorMsgs = new List<string>();
        //    var isValid = true;
        //    var properties = typeof(PaymentDetail).GetProperties();
        //    foreach (var property in properties)
        //    {
        //        // Lấy các thông tin thuộc tính
        //        var propertyName = property.Name;
        //        var propertyDisplay = propertyName;
        //        var propertyValue = property.GetValue(entity);
        //        var notEmpty = property.GetCustomAttributes(typeof(NotEmpty), true);
        //        var propName = property.GetCustomAttributes(typeof(PropertyName), true);
        //        var propDuplicate = property.GetCustomAttributes(typeof(NotDuplicate), true);
        //        var propCheckDate = property.GetCustomAttributes(typeof(CheckDate), true);
        //        var propCheckCode = property.GetCustomAttributes(typeof(CheckInsertCode), true);

        //        // lấy tên PropertyName
        //        if (propName.Length > 0)
        //        {
        //            propertyDisplay = (propName[0] as PropertyName).name;
        //        }
        //        // nếu như thuộc tính hiện tại có [NotEmpty]
        //        if (notEmpty.Length > 0)
        //        {
        //            // Nếu value là null hoặc empty
        //            if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString().Trim()))
        //            {
        //                //errorMsgs.Add(string.Format(Properties.Resources.NullValue, propertyDisplay));
        //                result.IsValid = false;
        //                result.ValidateInfo.Add(new ValidateField() { FieldName = propertyDisplay, ErrorCode = (int)Enum.Number.Number_1, ErrorMessage = Properties.Resources.NullValue });
        //            }
        //            // Nếu không gửi mã id phòng ban thì nó sẽ thực hiện validate ở đây
        //            var checkValueGuid = new Guid();
        //            if (property.PropertyType == typeof(Guid) && propertyValue.Equals(checkValueGuid))
        //            {
        //                //errorMsgs.Add(string.Format(Properties.Resources.NullValue, propertyDisplay));
        //                result.IsValid = false;
        //                result.ValidateInfo.Add(new ValidateField() { FieldName = propertyDisplay, ErrorCode = (int)Enum.Number.Number_1, ErrorMessage = Properties.Resources.NullValue });
        //            }
        //        }
                

        //        // TH có checkdate
        //        if (propCheckDate.Length > 0 && propertyValue != null)
        //        {
        //            var date = DateTime.Parse(propertyValue.ToString());
        //            var today = DateTime.Now;
        //            if (DateTime.Compare(date, today) > 0)
        //            {
        //                //errorMsgs.Add(string.Format(Properties.Resources.DateMessage, propertyDisplay));
        //                result.IsValid = false;
        //                result.ValidateInfo.Add(new ValidateField() { FieldName = propertyDisplay, ErrorCode = (int)Enum.Number.Number_3, ErrorMessage = Properties.Resources.DateMessage });
        //            }

        //        }
        //    }
        //    return result;
        //}

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
