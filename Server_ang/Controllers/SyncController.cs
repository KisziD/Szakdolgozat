using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Server.Model;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncController : ControllerBase
    {

        public class Sync
        {
            public bool state;
            public int temp;
            public Sync()
            {
                state = System.Environment.GetEnvironmentVariable("SYNC") == "1";
                temp = Int32.Parse(System.Environment.GetEnvironmentVariable("SYNCTEMP"));
            }
            public string String()
            {
                return state + "_ _" + temp;
            }
        }

        public SyncController()
        {
            
        }

        [HttpGet]
        public string Get()
        {
            Sync sync = new Sync();
            return "{\"state\":\"" + sync.state.ToString().ToLower() +"\", \"temp\":\""+sync.temp+"\"}";
        }

        [HttpGet("settemp/{temp}")]
        public void setTemp(int _temp)
        {
            if(_temp <=45 && _temp >= 15)
            {
                System.Environment.SetEnvironmentVariable("SYNCTEMP", _temp.ToString());
                
            }
        }

        [HttpGet("toggle")]
        public int toggleSync()
        {
            if (System.Environment.GetEnvironmentVariable("SYNC") == "0")
            {
                System.Environment.SetEnvironmentVariable("SYNC", "1");
                return 1;
            }
            else
            {
                System.Environment.SetEnvironmentVariable("SYNC", "0");
                return 0;
            }
        }
    }
}
