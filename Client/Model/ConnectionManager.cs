using RestSharp;

namespace Client.Model
{
    public static class ConnectionManager
    {

        public static async void Login()
        {
            string? address = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(2).ToString() +":"+ Environment.GetEnvironmentVariable("PORT");
            string? server_address = Environment.GetEnvironmentVariable("SERVER_ADDRESS");
            var options = new RestClientOptions("http://" + server_address+"/api/")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            RestClient _client = new RestClient(options);
            RestRequest req = new RestRequest("client/address/"+address);
            req.RequestFormat = DataFormat.Json;
            req.Method = Method.Get;
            RestResponse resp =  _client.Get(req);
            Environment.SetEnvironmentVariable("CLIENT_ID", resp.Content);
            Console.WriteLine("Login to "+server_address +" using " + address + "   ID is: " + Environment.GetEnvironmentVariable("CLIENT_ID"));
        }
    }
}
