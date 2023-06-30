//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using Shop.UsersApi.Data;
using Shop.UsersApi.Models;

namespace Shop.UsersApi
{
    public class SeedData
    {
        public static async Task EnsureSeedData(IServiceScope scope, IConfiguration configuration, ILogger logger)
        {
            var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
            await context.Database.MigrateAsync().ConfigureAwait(false);
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleMenager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleMenager.RoleExistsAsync("Admin"))
            {
                var CreatingRole = new IdentityRole();
                CreatingRole.Id = Guid.NewGuid().ToString();
                CreatingRole.Name = "Admin";
                await roleMenager.CreateAsync(CreatingRole);
            }

            if (!await roleMenager.RoleExistsAsync("User"))
            {
                var CreatingRole = new IdentityRole();
                CreatingRole.Id = Guid.NewGuid().ToString();
                CreatingRole.Name = "Basic";
                CreatingRole.NormalizedName = "BASIC";
                await roleMenager.CreateAsync(CreatingRole);
            }

            if (await userMgr.FindByNameAsync("admin") == null)
            {
                var CreatingUser = new ApplicationUser();
                CreatingUser.Id = Guid.NewGuid().ToString();
                CreatingUser.EmailConfirmed = true;
                CreatingUser.Email = "admin@admin.pl";
                CreatingUser.FirstName = "Admin";
                CreatingUser.UserName = "admin";
                CreatingUser.LastName = "Admin";
                CreatingUser.PhoneNumber = "1234567890";
                CreatingUser.LockoutEnabled = false;
                CreatingUser.TwoFactorEnabled = false;
                await userMgr.CreateAsync(CreatingUser, "Admin123!");
                await userMgr.AddToRoleAsync(CreatingUser, "Admin");
            }

            if (await userMgr.FindByNameAsync("user") == null)
            {
                var CreatingUser = new ApplicationUser();
                CreatingUser.Id = Guid.NewGuid().ToString();
                CreatingUser.EmailConfirmed = true;
                CreatingUser.Email = "user@user.pl";
                CreatingUser.FirstName = "User";
                CreatingUser.UserName = "user";
                CreatingUser.LastName = "User";
                CreatingUser.PhoneNumber = "1234567890";
                CreatingUser.LockoutEnabled = false;
                CreatingUser.TwoFactorEnabled = false;
                await userMgr.CreateAsync(CreatingUser, "User123!");
                await userMgr.AddToRoleAsync(CreatingUser, "Basic");
            }
        }
    }
}