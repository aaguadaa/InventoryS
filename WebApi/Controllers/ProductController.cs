using Business.Contracts;
using Business.Implementation;
using Domain;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult Create([FromBody] Product product)
        {
            if (product == null) return BadRequest("Request is null");
            int id = _productService.Add(product);
            if (id < 0) return BadRequest("Unable to Create User");
            var payload = new { Id = id };
            return Ok(payload);
        }


        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Product product = _productService.Get(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody] Product product)
        {
            if (product == null) return BadRequest("Request is null");
            product.Id = id;
            bool updated = _productService.Update(product);
            if (!updated) return BadRequest("Unable to update User");
            return Ok(product);
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            bool deleted = _productService.Delete(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}