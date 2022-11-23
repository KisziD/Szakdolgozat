using RestSharp;

namespace Client.Model
{
    public static class ConnectionManager
    {

        public static async void Login()
        {
            string? address = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(1).ToString() +":"+ Environment.GetEnvironmentVariable("PORT");
            string? id = Environment.GetEnvironmentVariable("CLIENT_ID");
            string? server_address = Environment.GetEnvironmentVariable("SERVER_ADDRESS");
            var options = new RestClientOptions("http://" + server_address)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            RestClient _client = new RestClient(options);
            RestRequest req;
            if (id != null)
            {
                 req = new RestRequest("client/id/" + id);
            }
            else
            {
                 req = new RestRequest("client/address/" + address);
            }
            
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.Get;
            RestResponse resp =  _client.Get(req);

            if (resp.Content == "\"id_err\"")
            {
                Environment.SetEnvironmentVariable("CLIENT_ID", null);
                id = null;
                req = new RestRequest("client/address/" + address);
                resp = _client.Get(req);
            }

            if(id == null)
            {
                Console.WriteLine(resp.Content);
                Environment.SetEnvironmentVariable("CLIENT_ID", resp.Content);
                id=resp.Content;
            }
            else
            {   
                
                if(resp.Content == address)
                {
                    Console.WriteLine("Login to " + server_address + " using " + address + "   ID is: " + Environment.GetEnvironmentVariable("CLIENT_ID"));
                }
                else
                {
                    Console.WriteLine("Set address for "+id+"_"+address);
                    req = new RestRequest("client/setaddr/"+id+"_"+address);
                    req.RequestFormat = DataFormat.Json;
                    req.Method = Method.Get;
                    _client.Get(req);
                }
            }

            //Console.WriteLine("Login to "+server_address +" using " + address + "   ID is: " + Environment.GetEnvironmentVariable("CLIENT_ID"));
        }
    }
}
