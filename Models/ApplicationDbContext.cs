using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models.Account;

namespace PUP_Online_Lagoon_System.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}
