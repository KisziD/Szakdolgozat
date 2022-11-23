using Microsoft.AspNetCore.Mvc;
using Server.Model;
using Server.Contexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
 

    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
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
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return databaseContext.Clients.ToArray();
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

        [HttpGet("setaddr/{newaddress}")]
        public int SetAddress(string newaddress)
        {
            string id = newaddress.Split("_")[0];
            string address = newaddress.Split("_")[1];
            Console.WriteLine(address);
            Client c = databaseContext.Clients.Where(c => c.ClientID == Int32.Parse(id)).FirstOrDefault();
            if (c != null) //check database
            {   //add client
                databaseContext.Clients.Where(c => c.ClientID == Int32.Parse(id)).FirstOrDefault().ClientAddress = address;
                databaseContext.SaveChanges();
            }
            return 0;

        }
        [HttpGet("id/{id}")]
        public string getAddress(int id)
        {
            Client c = databaseContext.Clients.Where(c => c.ClientID == id).FirstOrDefault();
            if (c == null)
            {
                return "id_err";
            }else
            {
                Console.WriteLine(c.ClientID + ":  " + c.ClientAddress);
                return c.ClientAddress;
            }
        }

        [HttpGet("edit/{stru}")]
        public void edit(string stru)
        {
            int id = Int32.Parse(stru.Split("_")[0]);
            string newName = stru.Split("_")[1];
            string newAddress = stru.Split("_")[2];
            Client c = databaseContext.Clients.Where(c => c.ClientID == id).FirstOrDefault();
            if (c != null) //check database
            {   //add client
                c.ClientName = newName;
                c.ClientAddress = newAddress;
                databaseContext.SaveChanges();
            }
        }

        // PUT api/<ClientController>/5
        [HttpGet("settemp/{tempstr}")]
        public void Put(string tempstr)
        {
            string id = tempstr.Split("_")[0];
            int temp = Int32.Parse(tempstr.Split("_")[1]);
            Client c = databaseContext.Clients.Where(c => c.ClientID == Int32.Parse(id)).FirstOrDefault();
            Console.WriteLine(tempstr);
            if (c != null) //check database
            {   //add client
                databaseContext.Clients.Where(c => c.ClientID == Int32.Parse(id)).FirstOrDefault().TargetTemp = temp;
                databaseContext.SaveChanges();
            }
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            databaseContext.Remove(id);
            databaseContext.SaveChanges();
        }

        
    }
}
