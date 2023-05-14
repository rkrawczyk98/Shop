using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Services;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Services;
//using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.DependencyResolver
{
    public static class DependencyResolverService
    {
        public static void Register(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer("name=ConnectionString:ShopDb",
                x => x.MigrationsAssembly("Shop.DbMigrations")));
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ShopDbContext>>();
            using (var dbContext = new ShopDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
