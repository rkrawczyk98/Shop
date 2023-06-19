using Microsoft.EntityFrameworkCore;
using Shop.OrdersApi.Models;

namespace Shop.OrdersApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}