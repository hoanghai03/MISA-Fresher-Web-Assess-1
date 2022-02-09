using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var res = _baseService.GetService();
                return Ok(res);
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Data);

            }
        }

        /// <summary>
        /// lấy entity theo mã id
        /// </summary>
        /// <returns>trả về: 200 - thành công </returns>
        /// createdBy 31/12/2021
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {

            try
            {
                var res = _baseService.GetByIdService(entityId);
                return Ok(res);
            }
            catch(HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Data);

            }

        }


        /// <summary>
        /// thêm mới entity
        /// </summary>
        /// <returns>trả về: 201 - thêm mới thành công </returns>
        /// createdBy 31/12/2021

        [HttpPost]
        public IActionResult Post(T entity)
        {
            try
            {
                var res = _baseService.InsertService(entity);
                if (res != null)
                {
                    return StatusCode((int)HttpStatusCode.Created, res);
                }
                return null;
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Data);
            }


        }

        /// <summary>
        /// cập nhật entity theo id
        /// </summary>
        /// <returns>trả về: 201 - cập nhật thành công </returns>
        /// createdBy 31/12/2021

        [HttpPut("{entityId}")]
        public IActionResult Put(T entity, Guid entityId)
        {
            try
            {
                var res = _baseService.UpdateService(entity, entityId);
                if (res > 0)
                {
                    return StatusCode((int)HttpStatusCode.Created, res);
                }
                return null;
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Data);

            }

        }

        /// <summary>
        /// xóa nhân viên theo id
        /// </summary>
        /// <returns>trả về: 200-xóa thành công </returns>
        /// createdBy 31/12/2021

        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                 var res = _baseService.DeleteService(entityId);
                if (res > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, res);
                }
                return null;
            }
            catch (HttpResponseException ex)
            {
                throw new HttpResponseException(ex.Value);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Data);

            }
        }

    }
}
