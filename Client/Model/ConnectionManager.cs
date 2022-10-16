using RestSharp;

namespace Client.Model
{
    public static class ConnectionManager
    {

        public static void Login()
        {
             string? port = Environment.GetEnvironmentVariable("PORT");
             string? address = Environment.GetEnvironmentVariable("ADDRESS");
             string? server_address = Environment.GetEnvironmentVariable("SERVER_ADDRESS");
             RestClient _client = new RestClient("http://"+server_address);
            //_client.PostAsync(new RestRequest("api/client/" + port));
            Console.WriteLine("Login to "+server_address +" using " + address +":"+port);
        }
    }
}
