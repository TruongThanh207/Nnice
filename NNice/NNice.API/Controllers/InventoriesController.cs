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
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventory;
        public InventoriesController(IInventoryService inventory)
        {
            _inventory = inventory;
        }
        // GET: api/Inventories
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var inventories = await _inventory.GetAllAsync();
            if (inventories == null || inventories.Count() == 0)
            {
                return NotFound(new ResponseObject()
                {
                    Success = false,
                    Message = "Can not find the inventories",
                    Code = HttpStatusCode.OK
                });
            }

            return Ok(new ResponseObject<InventoryDTO>()
            {
                data = inventories
            });
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var inventory = await _inventory.GetByIdAsync(id);
            if (inventory != null)
                return Ok(new ResponseObject<InventoryDTO>
                {
                    data = new List<InventoryDTO>() { inventory }
                });

            return NotFound(new ResponseObject()
            {
                Success = false,
                Message = $"Can not find the inventory {id}",
                Code = HttpStatusCode.NotFound
            });
        }

        // POST: api/Inventories
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] InventoryDTO input)
        {
            await _inventory.CreateAsync(input);
            return Ok(new ResponseObject());
        }

        // PUT: api/Inventories/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
