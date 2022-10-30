using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.Contexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
 

    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private Client? client;
        private DatabaseContext databaseContext;
        public ClientController(DatabaseContext _context)
        {
            databaseContext = _context;
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
        
        [HttpGet("address/{address}")]
        public int Post(string address)
        {            
            Client c = databaseContext.Clients.Where(c=>c.ClientAddress == address).FirstOrDefault();
            if (c == null) //check database
            {   //add client
                databaseContext.Clients.Add(new Client(address));
                databaseContext.SaveChanges();
            }
            return databaseContext.Clients.Where(c => c.ClientAddress ==address).FirstOrDefault().ClientID;
           
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
