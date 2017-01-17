using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using rabbitmq_demo;
using RabbitMQ.Client;

namespace Webshop.API.Controllers
{
    [Route("api/[controller]")]
    public class BestellingenController : Controller
    {
        private ISender _sender;

        public BestellingenController(ISender sender)
        {
            _sender = sender;
        }

        // GET api/bestellingen
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/bestellingen/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/bestellingen
        [HttpPost]
        public void Post([FromBody]Bestelling bestelling)
        {
            var bestellingKeuren = new BestellingKeuren
            {
                Id =  bestelling.Id
            };

            _sender.PublishCommand(bestellingKeuren);
        }

        // PUT api/bestellingen/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bestellingen/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
