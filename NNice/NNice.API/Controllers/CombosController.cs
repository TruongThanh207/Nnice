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
    public class CombosController : ControllerBase
    {
        private readonly IComboService _comboService;
        public CombosController(IComboService comboService)
        {
            _comboService = comboService;
        }
        // GET: api/Combos
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var comboDTOs = await _comboService.GetAllAsync();
            if (comboDTOs == null || comboDTOs.Count() == 0)
            {
                return NotFound(new ResponseObject()
                {
                    Success = false,
                    Message = "Can not found the products",
                    Code = HttpStatusCode.NotFound
                });
            }

            return Ok(new ResponseObject<ComboDTO>()
            {
                data = comboDTOs
            });
        }

        // GET: api/Combos/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var combo = await _comboService.GetByIdAsync(id);
            if (combo != null)
                return Ok(new ResponseObject<ComboDTO>
                {
                    data = new List<ComboDTO>() { combo }
                });

            return NotFound(new ResponseObject()
            {
                Success = false,
                Message = $"Can not found the product {id}",
                Code = HttpStatusCode.NotFound
            });
        }

        // POST: api/Combos
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ComboDTO input)
        {
            await _comboService.CreateAsync(input);
            return Ok(new ResponseObject());
        }

        // PUT: api/Combos/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] ComboDTO input)
        {
            await _comboService.UpdateAsync(input, id);
            return Ok(new ResponseObject());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _comboService.DeleteAsync(id);
            return Ok(new ResponseObject());
        }
    }
}
