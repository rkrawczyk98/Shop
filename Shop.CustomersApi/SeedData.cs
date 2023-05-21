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
            //var admin = await userMgr.FindByNameAsync("admin");
            //var user = await userMgr.FindByNameAsync("user");
            //if (admin == null || user == null)
            //{
            //    logger.LogInformation("Generating inbuilt accounts");

            //    if (admin == null)
            //    {
            //        var Admin = new ApplicationUser
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            UserName = "admin",
            //            Email = "admin@admin.pl",
            //            EmailConfirmed = true,
            //            IsEnabled = true,
            //            IsDeleted = false,
            //            CreatedOn = DateTime.Now,
            //            Password = "Admin123!",
            //            FirstName = "admin",
            //            LastName = "admin",
            //            LockoutEnabled = false
            //        };
            //        var result = userMgr.CreateAsync(Admin, "Admin123!").Result;

            //        if (!result.Succeeded)
            //        {
            //            throw new Exception(result.Errors.ToString());
            //        }
            //        logger.LogDebug("Admin created");
            //    }
            //    else logger.LogDebug("User named 'admin' alredy exist.");

            //    if (user == null)
            //    {
            //        var User = new ApplicationUser 
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            UserName = "user",
            //            Email = "user@user.pl",
            //            EmailConfirmed = true,
            //            IsEnabled = true,
            //            IsDeleted = false,
            //            CreatedOn = DateTime.Now,
            //            Password = "User123!",
            //            FirstName = "user",
            //            LastName = "user",
            //            LockoutEnabled = false
            //        };
            //        var result = userMgr.CreateAsync(User, "User123!").Result;

            //        if (!result.Succeeded)
            //        {
            //            throw new Exception(result.Errors.ToString());
            //        }
            //        logger.LogDebug("User created");
            //    }
            //    else logger.LogDebug("User named 'user' already exist.");

            //    logger.LogInformation("Inbuilt account generation completed");
            //}
            //if (await roleMenager.FindByNameAsync("Admin") == null)
            //{
            //    var adminRole = new ApplicationRole 
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Admin",
            //        CreatedOn= DateTime.Now
            //    };
            //    var result = await roleMenager.CreateAsync(adminRole);
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.ToString());
            //    }
            //}

            //if (await roleMenager.FindByNameAsync("Basic") == null)
            //{
            //    var basicRole = new ApplicationRole
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name = "Basic",
            //        CreatedOn = DateTime.Now
            //    };
            //    var result = await roleMenager.CreateAsync(basicRole);
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.ToString());
            //    }
            //}


            if (!await roleMenager.RoleExistsAsync("Admin"))
            {
                var CreatingRole = new IdentityRole();
                CreatingRole.Id= Guid.NewGuid().ToString();
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
                await userMgr.CreateAsync(CreatingUser,"Admin123!");
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
            //ApplicationUser adminCreated = await userMgr.FindByNameAsync("admin");
            //if (!await userMgr.IsInRoleAsync(adminCreated, "Admin"))
            //{
            //    var result = await userMgr.AddToRoleAsync(adminCreated,"Admin");
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.ToString());
            //    }
            //}

            //if (userMgr.FindByNameAsync("user").Result.Roles.IsNullOrEmpty() == true)
            //{

            //    var userCreated = await userMgr.FindByNameAsync("user");
            //    var result = await userMgr.AddToRoleAsync(userCreated, "BASIC");
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.ToString());
            //    }
            //}
        }
    }
}
