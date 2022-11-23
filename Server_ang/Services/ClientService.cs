using Server.Contexts;
using Server.Model;
using RestSharp;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace Server.Services
{
    public class ClientService
    {
        private DatabaseContext dbc;

        public static System.Timers.Timer aTimer = new System.Timers.Timer();

        public ClientService(DatabaseContext dbc)
        {
            this.dbc = dbc;
        }

        public void SetTimer()
        {
            aTimer = new System.Timers.Timer(15000/*60000*//*5000*/);
            aTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            Console.WriteLine("started timed events");
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Client temps: ");
            updateClientTemps();
            updateClientValves();

        }

        private void updateClientTemps()
        {
            RestClient _client;
            RestRequest req;
            RestResponse res;

            foreach (var c in dbc.Clients)
            {
                string address = "localhost:5082"; //c.ClientAddress;
                try
                {
                    req = new RestRequest("temp");
                    _client = new RestClient("http://" + address);
                    res = _client.Get(req);
                    Console.WriteLine(res.Content);
                    c.CurrentTemp = Int32.Parse(res.Content);
                    Console.WriteLine(c.ClientName + ": " + c.CurrentTemp + "  C");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            dbc.SaveChanges();

        }

        private void updateClientValves()
        {
            RestClient _client;
            RestRequest req;

           
            foreach (var c in dbc.Clients)
            {
                string address = "localhost:5082"; //c.ClientAddress;
                try
                {
                    _client = new RestClient("http://"+address);
                    if (Predicate(c.TargetTemp,c.CurrentTemp))
                    {
                        req = new RestRequest("valve/open");
                        _client.Get(req);
                    }
                    else
                    {
                        req = new RestRequest("valve/close");
                        _client.Get(req);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
           
            dbc.SaveChanges();

        }

        private bool Predicate(int target, int current)
        {
            if (System.Environment.GetEnvironmentVariable("SYNC") == "0")
            {
                Console.WriteLine("Target: "+target+"C");
                return target>=current;
            }
            else
            {
                int syncTarget = Int32.Parse(System.Environment.GetEnvironmentVariable("SYNCTEMP"));
                Console.WriteLine("syncTarget: " + syncTarget + "C");
                return syncTarget>=current;
            }
        }
    }
}
