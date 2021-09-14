using Microsoft.AspNetCore.Mvc;
using ShipsAndPorts.Core.Models.ApiModels;
using ShipsAndPorts.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace ShipsAndPorts.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        private readonly IShipService shipService;

        public ShipController(IShipService shipService)
        {
            this.shipService = shipService;
        }

        // GET: api/<ShipController>
        [HttpGet]
        public ActionResult<IList<ShipApiModel>> Get()
        {
            return Ok(shipService.GetAll().ToList());
        }

        // GET api/<ShipController>/5
        [HttpGet("{id}")]
        public ActionResult<ShipApiModel> Get(int id)
        {
            return Ok(shipService.Get(id));
        }

        // POST api/<ShipController>
        [HttpPost]
        public ActionResult Post([FromBody] ShipApiModel value)
        {
            shipService.Add(value);
            return Ok("Added");
        }

        // PUT api/<ShipController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ShipApiModel value)
        {
            shipService.Update(value);
            return Ok("Updated");
        }

        [HttpPost]
        public ActionResult UpdateVelocity([FromBody] string shipId, float velocity)
        {
            if(string.IsNullOrEmpty(shipId))
                return BadRequest("You should not pass empty shipId");

            if(velocity <= 0)
                return BadRequest("Speed should be more than 0");

            shipService.UpdateVelocity(shipId, velocity);
            return Ok("Velocity updated");
        }

        [HttpGet]
        public ActionResult ClosestPort(string shipId)
        {
            if (string.IsNullOrEmpty(shipId))
                return BadRequest("You should not pass empty shipId");

            var result = shipService.GetClosestPort(shipId);
            return Ok(result);
        }

        // DELETE api/<ShipController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            shipService.Delete(id);
            return Ok("Deleted");
        }
    }
}
