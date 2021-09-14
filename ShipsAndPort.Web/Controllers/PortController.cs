using Microsoft.AspNetCore.Mvc;
using ShipsAndPorts.Core.Models.ApiModels;
using ShipsAndPorts.Core.Services;
using System.Collections.Generic;


namespace ShipsAndPorts.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortController : ControllerBase
    {
        private readonly IPortService portService;

        public PortController(IPortService portService)
        {
            this.portService = portService;
        }

        // GET: api/<PortController>
        [HttpGet]
        public ActionResult<IList<PortApiModel>> Get()
        {
            return Ok(portService.GetAll());
        }

        // GET api/<PortController>/5
        [HttpGet("{id}")]
        public ActionResult<PortApiModel> Get(int id)
        {
            return Ok(portService.Get(id));
        }

        // POST api/<PortController>
        [HttpPost]
        public ActionResult Post([FromBody] PortApiModel value)
        {
            portService.Add(value);
            return Ok("Added");
        }

        // PUT api/<PortController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PortApiModel value)
        {
            portService.Update(value);
            return Ok("Updated");
        }

        // DELETE api/<PortController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            portService.Delete(id);
            return Ok("Deleted");
        }
    }
}
