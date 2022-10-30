using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Server.Model;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienttempController : ControllerBase
    {
        readonly RestClient restClient;

        public ClienttempController()
        {
            restClient = new RestClient("http://127.0.0.1:5082");
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
         => await restClient.GetAsync<IEnumerable<string>>(new RestRequest("api/temp"));
         

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
