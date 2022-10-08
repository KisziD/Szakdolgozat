using RestSharp;

namespace Client.Model
{
    public class ConnectionManager
    {
        public int port = 5082;
        readonly RestClient _client;

        public ConnectionManager()
        {
            _client = new RestClient("http://127.0.0.1:5147");
        }

        public int Login()
        {
            _client.PostAsync(new RestRequest("api/client/" + port));
            Console.WriteLine("Login");
            return 0;
        }
    }


}
