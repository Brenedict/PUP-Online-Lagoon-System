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
            // User -> Vendor (Cascade)
            modelBuilder.Entity<Vendor>()
                .HasOne(v => v.User)
                .WithOne() // Or WithMany
                .HasForeignKey<Vendor>(v => v.User_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FoodItem>()
                .HasOne(f => f.Stall)
                .WithMany() 
                .HasForeignKey(f => f.Stall_ID)
                .OnDelete(DeleteBehavior.Cascade); // Delete Stall = Delete Food

            modelBuilder.Entity<ItemOrder>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.Customer_ID)
                .OnDelete(DeleteBehavior.SetNull); // Delete User = Keep Order, Nullify ID

            modelBuilder.Entity<ItemOrder>()
                .HasOne(o => o.Stall)
                .WithMany()
                .HasForeignKey(o => o.Stall_ID)
                .OnDelete(DeleteBehavior.SetNull); // Delete Stall = Keep Order, Nullify ID

            modelBuilder.Entity<OrderDetails>()
                .HasOne(d => d.Order)
                .WithMany() 
                .HasForeignKey(d => d.Order_ID)
                .OnDelete(DeleteBehavior.Cascade); // If order is wiped, details are wiped

            modelBuilder.Entity<OrderDetails>()
                .HasOne(d => d.Food)
                .WithMany()
                .HasForeignKey(d => d.Food_ID)
                .OnDelete(DeleteBehavior.SetNull); // If food is deleted, history remains

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Customer>(c => c.User_ID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
