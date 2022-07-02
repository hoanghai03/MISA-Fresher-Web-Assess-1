using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Core.Entities.Base;
using MISA.Fresher.Core.Exceptions;
using MISA.Fresher.Core.Interfaces.Infrastructure;
using MISA.Fresher.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MISA.Fresher.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController<T> : ControllerBase
    {
        IBaseService<T> _baseService;
        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// lấy toàn bộ entity
        /// </summary>
        /// <returns>trả về: 200 - thành công </returns>
        /// createdBy 31/12/2021
        [HttpGet]
        public IActionResult Get()
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _baseService.GetService();

            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);
            }
            return Ok(serviceResult);
        }

        /// <summary>
        /// lấy entity theo mã id
        /// </summary>
        /// <returns>trả về: 200 - thành công </returns>
        /// createdBy 31/12/2021
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                serviceResult = _baseService.GetByIdService(entityId);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);

            }
            return Ok(serviceResult);


        }


        /// <summary>
        /// thêm mới entity
        /// </summary>
        /// <returns>trả về: 201 - thêm mới thành công </returns>
        /// createdBy 31/12/2021

        [HttpPost]
        public IActionResult Post(T entity)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                serviceResult = _baseService.InsertService(entity);

            }

            catch (Exception ex)
            {
                serviceResult.SetError(ex);
            }


            return Ok(serviceResult);



        }

        /// <summary>
        /// cập nhật entity theo id
        /// </summary>
        /// <returns>trả về: 201 - cập nhật thành công </returns>
        /// createdBy 31/12/2021

        [HttpPut("{entityId}")]
        public IActionResult Put(T entity, Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                serviceResult = _baseService.UpdateService(entity, entityId);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);

            }
            return Ok(serviceResult);


        }

        /// <summary>
        /// xóa nhân viên theo id
        /// </summary>
        /// <returns>trả về: 200-xóa thành công </returns>
        /// createdBy 31/12/2021

        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                serviceResult = _baseService.DeleteService(entityId);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);

            }
            return Ok(serviceResult);
        }

        /// <summary>
        /// Hàm phân trang dữ liệu
        /// </summary>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">vị trí trang</param>
        /// <param name="filter">chuỗi filter</param>
        /// <returns>trả về số bản ghi của trang</returns>
        /// createdBy NHHAi 10/1/2021
        [HttpPost("filter")]
        public IActionResult Filter(PaginationRequest paginationRequest)
        {
            ServiceResult serviceResult = new ServiceResult();
            try
            {
                // Hàm gọi đến hàm lấy mã nhân viên ở service
                serviceResult = _baseService.FilterService(paginationRequest);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);
            }
            // trả về mã mới
            return Ok(serviceResult);
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">danh sách id cần xóa</param>
        /// <returns></returns>
        /// createdBY NHHai 10/1/2021
        [HttpDelete("all")]
        public IActionResult DeleteAll(List<string> entityIds)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                serviceResult = _baseService.DeleteAllService(entityIds);
            }
            catch (Exception ex)
            {
                serviceResult.SetError(ex);

            }
            return Ok(serviceResult);
        }


        /// <summary>
        /// Hàm xuất khẩu dữ liệu ra excel
        /// </summary>
        /// <returns>file excel</returns>
        /// createdBy NHHAi 20/1/2022
        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            try
            {
                var res = _baseService.ExportListUsingEPPlus();
                // đặt tên file excel
                string excelName = $"Danh-sách-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                // file
                return File(res, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
        }

    }
}
