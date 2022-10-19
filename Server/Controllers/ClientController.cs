using Microsoft.AspNetCore.Mvc;
using Server.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
 

    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private Client client;
        public ClientController()
        {
        } 

        // GET: api/<ClientController>
       /* [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientController>/login
        
        [HttpPost("login")]
        public Client Post([FromBody]Client address)
        {
            //check database
            //add client
            Console.WriteLine(address.port);
            return address;
            //client.port = port;
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
