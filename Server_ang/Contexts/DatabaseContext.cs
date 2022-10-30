using Microsoft.EntityFrameworkCore;
using Server.Model;

namespace Server.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = null!;
        
    }
}
