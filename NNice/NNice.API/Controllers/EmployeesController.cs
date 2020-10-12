using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNice.Business.DTO;
using NNice.Business.Services;

namespace NNice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _emService;
        public EmployeesController(IEmployeeService emService)
        {
            _emService = emService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var employees = await _emService.GetAllAsync();
            if (employees == null || employees.Count() == 0)
            {
                return NotFound(new ResponseObject()
                {
                    Success = false,
                    Message = "Can not found the employees",
                    Code = HttpStatusCode.NotFound
                });
            }

            return Ok(new ResponseObject<EmployeeDTO>()
            {
                data = employees
            });
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var emDto = await _emService.GetByIdAsync(id);
            if (emDto != null)
                return Ok(new ResponseObject<EmployeeDTO>
                {
                    data = new List<EmployeeDTO>() { emDto }
                });
            return NotFound(new ResponseObject()
            {
                Success = false,
                Message = $"Can not found the employee {id}",
                Code = HttpStatusCode.NotFound
            });
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] EmployeeDTO input)
        {
            await _emService.CreateAsync(input);
            return Ok(new ResponseObject());
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] EmployeeDTO input)
        {
            await _emService.UpdateAsync(input);
            return Ok(new ResponseObject());
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _emService.DeleteAsync(new EmployeeDTO { ID = id });
            return Ok(new ResponseObject());
        }
    }
}
