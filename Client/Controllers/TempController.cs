using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TempController : ControllerBase
    {
        // GET: api/<TempController>
        [HttpGet]
        public int Get()
        {
            int temp = new Random().Next(20,40);

            return temp;
        }
    }
}
