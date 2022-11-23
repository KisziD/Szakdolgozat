using Microsoft.AspNetCore.Mvc;
using System.Device.Gpio;
using Client.Services;

namespace Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValveController : ControllerBase
    {
        ValveService valveService;
        public ValveController(ValveService vs)
        {
            valveService = vs;
        }

        // GET: api/<ValuesController>
        [HttpGet("open")]
        public void open()
        {
            Console.WriteLine("open");
        }

        [HttpGet("close")]
        public void close()
        {
          Console.WriteLine("close");
        }
    }
}
