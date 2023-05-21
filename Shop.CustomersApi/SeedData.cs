//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.UsersApi.Data;
using Shop.UsersApi.Interfaces;
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
            var roleMenager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            //var userRoleMaanger = scope.ServiceProvider.GetRequiredService<IdentityUserRole<>>
            var admin = await userMgr.FindByNameAsync("admin");
            var user = await userMgr.FindByNameAsync("user");
            if (admin == null || user == null)
            {
                logger.LogInformation("Generating inbuilt accounts");

                if (admin == null)
                {
                    var Admin = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "admin",
                        Email = "admin@admin.pl",
                        EmailConfirmed = true,
                        IsEnabled = true,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                        Password = "Admin123!",
                        FirstName = "admin",
                        LastName = "admin",
                        LockoutEnabled = false
                    };
                    var result = userMgr.CreateAsync(Admin, "Admin123!").Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.ToString());
                    }
                    context.Users.Add(Admin);
                    logger.LogDebug("Admin created");
                }
                else logger.LogDebug("User named 'admin' alredy exist.");

                if (user == null)
                {
                    var User = new ApplicationUser 
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "user",
                        Email = "user@user.pl",
                        EmailConfirmed = true,
                        IsEnabled = true,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                        Password = "User123!",
                        FirstName = "user",
                        LastName = "user",
                        LockoutEnabled = false
                    };
                    var result = userMgr.CreateAsync(User, "User123!").Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.ToString());
                    }
                    context.Users.Add(User);
                    logger.LogDebug("User created");
                }
                else logger.LogDebug("User named 'user' already exist.");

                logger.LogInformation("Inbuilt account generation completed");
            }

            if (await roleMenager.FindByNameAsync("Admin") == null)
            {
                var adminRole = new ApplicationRole 
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    CreatedOn= DateTime.Now
                };
                var result = await roleMenager.CreateAsync(adminRole);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }
                context.Roles.Add(adminRole);
            }

            if (await roleMenager.FindByNameAsync("Basic") == null)
            {
                var basicRole = new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Basic",
                    CreatedOn = DateTime.Now
                };
                var result = await roleMenager.CreateAsync(basicRole);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }
                context.Roles.Add(basicRole);
            }

            if ((userMgr.IsInRoleAsync(admin,"Admin")) == null) // Być może trzeba będzie dodać context.. cośtam Add/Update
            {
                var result = await userMgr.AddToRoleAsync(admin, "Admin");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }
            }

            if ((userMgr.IsInRoleAsync(user, "Basic")) == null) // Być może trzeba będzie dodać context.. cośtam Add/Update
            {
                var result = await userMgr.AddToRoleAsync(user, "Basic");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }
            }

            
            await context.SaveChangesAsync();
        }
    }
}
