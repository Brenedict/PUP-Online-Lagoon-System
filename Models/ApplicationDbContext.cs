using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;

namespace PUP_Online_Lagoon_System.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<ItemOrder> Orders{ get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<FoodStall> FoodStalls { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FoodStall>()
                .HasOne(s => s.Vendor)
                .WithOne(v => v.Stall)
                .HasForeignKey<FoodStall>(s => s.Vendor_ID)
                .IsRequired(false); 
        }
    }
}
