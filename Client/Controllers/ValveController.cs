using Microsoft.AspNetCore.Mvc;
using System.Device.Gpio;
using Client.Services;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValveController : ControllerBase
    {
        ValveService valveService;
        public ValveController(ValveService vs)
        {
            valveService = vs;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //return valveState;
            return new string[] { "value1", "value2" };
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //set valveState
            valveService.setValveState(value == "true");
        }
    }
}
