using Microsoft.AspNetCore.Mvc;
using MongoDb.Api.Interfaces;
using MongoDb.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MongoDbController : ControllerBase
    {
        private readonly IServiceInterface serviceInterface;

        public MongoDbController(IServiceInterface serviceInterface)
        {
            this.serviceInterface = serviceInterface;
        }
        [HttpGet]
        public Task<List<Student>> Get()
        {
            try
            {
                var res = serviceInterface.GetAsync();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("id")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            try
            {
                var res = await serviceInterface.GetByIdAsync(id);
                return res;

            }
            catch (Exception)
            {

                throw;
            }
        }        
        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await serviceInterface.DeleteAsync(id);
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]string id,[FromBody]Student student)
        {
            try
            {
                await serviceInterface.UpdateAsync(id,student);
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }        
        [HttpPost("insert")]
        public async Task<IActionResult> Insert(Student student)
        {
            try
            {
                await serviceInterface.CreateAsync(student);
                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
