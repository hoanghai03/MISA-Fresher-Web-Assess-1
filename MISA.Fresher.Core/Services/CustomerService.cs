using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Exceptions;
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
    /// customer service
    /// createdBy NHHai 28/12/2021
    /// </summary>
    public class CustomerService : ICustomerService
    {

        ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public int? Insert(Customer customer)
        {
            // khai báo danh sách lỗi:
            List<string> errorMsgs = new List<string>();
            var customerCode = customer.CustomerCode;
            // + mã khách hàng không được phép để trống
            if (string.IsNullOrEmpty(customerCode))
            {
                errorMsgs.Add("Mã khách hàng đã bị trống");
            }

            // + số điện thoại không được phép để trống
            if (string.IsNullOrEmpty(customer.PhoneNumber))
            {
                errorMsgs.Add("Số điện thoại không được để trống");
            }
            // + email không đúng định dạng
            // + ngày sinh không được lớn hơn ngày hiện tại
            // + Kiểm tra mã khách hàng có trùng hay không?
            var isDuplicate = _customerRepository.CheckCustomerCodeDuplicate(customer.CustomerCode);
            if (isDuplicate == true)
            {
                errorMsgs.Add("Mã khách hàng không được phép trùng!");
            }
            // Hiển thị các thông báo lỗi:
            if (errorMsgs.Count > 0)
            {
                var result = new
                {
                    userMsg = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại!",
                    data = errorMsgs
                };
                // đưa ra lỗi
                throw new HttpResponseException(result);
            }
            var res = _customerRepository.Insert(customer);
            return res;
        }

        public int? Update(Customer entity, Guid entityId)
        {
            throw new NotImplementedException();
        }
    }
}
