using Business.Contracts;
using Business.Implementation;
using Domain;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [RoutePrefix("product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {

            _productService = productService;
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Product product)
        {
            if (product == null) return BadRequest("Request is null");
            int id = await _productService.AddProductAsync(product);
            if (id < 0) return BadRequest("Unable to Create User");
            var payload = new { Id = id };
            return Ok(payload);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Product product)
        {
            if (product == null) return BadRequest("Request is null");
            product.Id = id;
            bool updated = await _productService.UpdateProductAsync(product);
            if (!updated) return BadRequest("Unable to update User");
            return Ok(product);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            bool deleted = await _productService.DeleteProductAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}