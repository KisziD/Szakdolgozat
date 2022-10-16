using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        // GET: api/<TempController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            int temp = new Random().Next(20,30);
            int szoba_sz = new Random().Next(0,4);
            string[] szobak = {"Nappali","Gyerekszoba","Konyha","Hálószoba","Fürdőszoba"};

            return new string[] { szobak[szoba_sz], temp.ToString() + " °C" };
        }
    }
}
