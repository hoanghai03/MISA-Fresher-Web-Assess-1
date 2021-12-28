using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;
        ICustomerRepository _customerRepository;
        public CustomersController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }
        /// <summary>
        /// thêm mới khách hàng
        /// </summary>
        /// <param name="customer">khách hàng</param>
        /// <returns>trả về giá trị 201 nêu thêm mới thành công</returns>
        /// createdBy NHHAi 28/12/2021
        [HttpPost]
        public IActionResult Insert(Customer customer)
        {

                var entities = _customerService.Insert(customer);
                return StatusCode(201,entities);

        }

        /// <summary>
        /// Hiển thị thông tin khách hàng
        /// </summary>
        /// <returns>trả về toàn bộ khách hàng</returns>
        /// createdBy NHHAi 28/12/2021
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entities = _customerRepository.Get();
                return StatusCode(200, entities);
            }
            catch (Exception ex)
            {
                //  TH: Xảy ra ngoại lệ
                var result = new
                {
                    msgDev = ex.Message,
                    userMsg = "Có lỗi xảy ra vui lòng liên hệ ",
                    data = DBNull.Value,
                    moreInfo = ""
                };
                // trả về lỗi
                return StatusCode(500, result);
            }

        }
    }
}
