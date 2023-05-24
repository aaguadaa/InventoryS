using Business.Contracts;
using Business.Implementation;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{

    [RoutePrefix("inventory")]
    public class InventoryController : ApiController
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {

            _inventoryService = inventoryService;
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] Domain.Model.Inventory l)
        {
            if (l == null) return BadRequest("Request is null");
            int id = _inventoryService.Add(l);
            if (id < 0) return BadRequest("Unable to Create User");
            var payload = new { Id = id };
            return Ok(payload);
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            bool deleted = _inventoryService.Delete(id);
            if (!deleted) return NotFound();
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Domain.Model.Inventory l = _inventoryService.Get(id);
            if (l == null) return NotFound();
            return Ok(l);
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody] Domain.Model.Inventory l)
        {
            if (l == null) return BadRequest("Request is null");
            l.Id = id;
            bool updated = _inventoryService.Update(l);
            if (!updated) return BadRequest("Unable to update User");
            return Ok(l);
        }

    }
}