using Microsoft.AspNet.Identity;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.UsersApi.Data;
using Shop.UsersApi.Interfaces;
using Shop.UsersApi.Models;
//using Shop.UsersApi.Services;

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

                if (admin != null)
                {
                    var Admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@admin.pl",
                        EmailConfirmed = true,
                        IsEnabled = true,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                        Password = "admin",
                        FirstName= "admin",
                        LastName= "admin"
                    };
                    var result = userMgr.CreateAsync(Admin, "admin").Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.ToString());
                    }
                    context.Users.Add(Admin);
                    logger.LogDebug("Admin created");
                }
                else logger.LogDebug("User named 'admin' alredy exist.");

                if (user != null)
                {
                    var User = new ApplicationUser 
                    {
                        UserName = "user",
                        Email = "user@user.pl",
                        EmailConfirmed = true,
                        IsEnabled = true,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now,
                        Password = "user",
                        FirstName = "user",
                        LastName = "user"
                    };
                    var result = userMgr.CreateAsync(User, "user").Result;

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

            if ((userMgr.IsInRoleAsync(admin.Id.ToString(),"Admin")) == null) // Być może trzeba będzie dodać context.. cośtam Add/Update
            {
                var result = await userMgr.AddToRoleAsync(admin.Id.ToString(), "Admin");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }
            }

            if ((userMgr.IsInRoleAsync(user.Id.ToString(), "Basic")) == null) // Być może trzeba będzie dodać context.. cośtam Add/Update
            {
                var result = await userMgr.AddToRoleAsync(user.Id.ToString(), "Basic");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }
            }

            
            await context.SaveChangesAsync();
        }
    }
}
