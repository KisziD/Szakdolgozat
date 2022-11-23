using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Model
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int ClientID { get; set; }
        public string ClientAddress { get; set; }
        public int CurrentTemp { get; set; }
        public int TargetTemp { get; set; }
        public string ClientName { get; set; }

        public Client() { }
        public Client(string address)
        {
            ClientAddress = address;
            CurrentTemp = 0;
            TargetTemp = 0;
            ClientName = "Client";
        }

    }
}
