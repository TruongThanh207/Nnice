using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNice.Business.DTO;
using NNice.Business.Services;

namespace NNice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartService _service;

        public ShoppingCartsController(IShoppingCartService service)
        {
            _service = service;
        }

        // GET: api/ShoppingCarts
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetCartItemsAsync();
            return Ok(new ResponseObject<CartDTO>()
            {
                data = result
            });
        }

        // GET: api/ShoppingCarts/EmptyCart
        [Route("EmptyCart")]
        [HttpGet]
        public async Task<IActionResult> EmptyCart()
        {
            await _service.EmptyCartAsync();
            return Ok(new ResponseObject<CartDTO>());
        }

        // POST: api/ShoppingCarts
        [Route("AddToCart")]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartDTO model)
        {
            await _service.AddToCartAsync(model);
            return Ok(new ResponseObject());
        }
        // POST: api/ShoppingCarts
        [HttpPost]
        [Route("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart([FromBody] CartDTO model)
        {
            await _service.RemoveFromCartAsync(model);
            return Ok(new ResponseObject());
        }

        // PUT: api/ShoppingCarts/5
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
