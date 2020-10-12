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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var products = await _productService.GetAllAsync();
            if (products == null || products.Count() == 0)
            {
                return NotFound(new ResponseObject()
                {
                    Success = false,
                    Message = "Can not find the products",
                    Code = HttpStatusCode.OK
                });
            }

            return Ok(new ResponseObject<ProductDTO>()
            {
                data = products
            });
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product != null)
                return Ok(new ResponseObject<ProductDTO>
                {
                    data = new List<ProductDTO>() { product }
                });
            return NotFound(new ResponseObject()
            {
                Success = false,
                Message = $"Can not find the product {id}",
                Code = HttpStatusCode.NotFound
            });
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ProductDTO input)
        {
            await _productService.CreateAsync(input);
            return Ok(new ResponseObject());
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] ProductDTO input)
        {
            await _productService.UpdateAsync(input, id);
            return Ok(new ResponseObject());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok(new ResponseObject());
        }
    }
}
