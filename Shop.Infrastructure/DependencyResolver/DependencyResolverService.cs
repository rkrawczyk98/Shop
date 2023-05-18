using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shop.Application.Core.Services;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Data.Seeding;
using Shop.Infrastructure.Services;
//using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.DependencyResolver
{
    public static class DependencyResolverService
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var assembyName = typeof(ShopDbContext).Namespace;
            services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(connectionString,
                optionsBuilder =>
                optionsBuilder.MigrationsAssembly(assembyName)));
            services.AddScoped<IProductService,ProductSerivce>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IWarehouseSerivce, WarehouseService>();
            services.AddTransient<IContextSeed, ContextSeed>();
        }

        public static async void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions =  serviceProvider.GetRequiredService<DbContextOptions<ShopDbContext>>();
            using (var dbContext = new ShopDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }

    }
}
