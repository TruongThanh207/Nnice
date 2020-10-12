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
    public class WorkShiftsController : ControllerBase
    {
        private readonly IWorkShiftService _wsService;
        public WorkShiftsController(IWorkShiftService wsService)
        {
            _wsService = wsService;
        }

        // GET: api/WorkShifts
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var workShifts = await _wsService.GetAllAsync();
            if (workShifts == null || workShifts.Count() == 0)
            {
                return NotFound(new ResponseObject()
                {
                    Success = false,
                    Message = "Can not found the workshifts",
                    Code = HttpStatusCode.NotFound
                });
            }

            return Ok(new ResponseObject<WorkShiftDTO>()
            {
                data = workShifts
            });
        }

        // GET: api/WorkShifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var wsDto = await _wsService.GetByIdAsync(id);
            if (wsDto != null)
                return Ok(new ResponseObject<WorkShiftDTO>
                {
                    data = new List<WorkShiftDTO>() { wsDto }
                });
            return NotFound(new ResponseObject()
            {
                Success = false,
                Message = $"Can not found the workshift {id}",
                Code = HttpStatusCode.NotFound
            });
        }

        // POST: api/WorkShifts
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] WorkShiftDTO input)
        {
            await _wsService.CreateAsync(input);
            return Ok(new ResponseObject());
        }

        // PUT: api/WorkShifts/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] WorkShiftDTO input)
        {
            await _wsService.UpdateAsync(input, id);
            return Ok(new ResponseObject());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _wsService.DeleteAsync(new WorkShiftDTO { ID = id });
            return Ok(new ResponseObject());
        }
    }
}
