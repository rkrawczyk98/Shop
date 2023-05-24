using Microsoft.EntityFrameworkCore;
using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryName);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Name);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}