using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Core.Services;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Services;
//using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.DependencyResolver
{
    public static class DependencyResolverService
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddDbContext<ShopDbContext>(options =>
            //options.UseSqlServer("name=ConnectionString:DefaultConnection",
            //    x => x.MigrationsAssembly("Shop.DbMigrations")));
            services.AddScoped<IProductService,ProductSerivce>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IWarehouseSerivce,WarehouseService>();
        }

        public static async void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions =  serviceProvider.GetRequiredService<DbContextOptions<ShopDbContext>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleMenager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await ContextSeed.SeedRolesAsync(userManager, roleMenager);
            using (var dbContext = new ShopDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
