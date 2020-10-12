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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: api/Orders
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var invoices = await _orderService.GetAllAsync();
            if (invoices == null || invoices.Count() == 0)
            {
                return Ok(new ResponseObject()
                {
                    Success = false,
                    Message = "Can not find the invoices",
                    Code = HttpStatusCode.OK
                });
            }

            return Ok(new ResponseObject<OrderDTO>()
            {
                data = invoices
            });
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var invoice = await _orderService.GetByIDAsync(id);
            if (invoice != null)
                return Ok(new ResponseObject<OrderDTO>
                {
                    data = new List<OrderDTO>() { invoice }
                });
            return NotFound(new ResponseObject()
            {
                Success = false,
                Message = $"Can not find the invoice {id}",
                Code = HttpStatusCode.NotFound
            });
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderDTO input)
        {
            return Ok(await _orderService.BookRoomAsync(input));
        }

        // PUT: api/Orders/5
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
