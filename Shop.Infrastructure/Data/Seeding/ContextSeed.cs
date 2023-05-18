using Castle.Core.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Application.Interfaces;
using Shop.Domain.Core.Models;
using Shop.Domain.Enums;
using Shop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data.Seeding
{
    public class ContextSeed : IContextSeed
    {
        private readonly ShopDbContext _context;
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;
        private List<string> permissionValues = new List<string>()
        {
            "users.view",
            "users.manage",
            "roles.view",
            "roles.manage",
            "roles.assign"
        };

        public ContextSeed(ShopDbContext context, AccountService accountService, ILogger<ContextSeed> logger)
        {
            _accountService = accountService;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string adminRoleName = "admin";
                const string userRoleName = "user";

                await EnsureRoleAsync(adminRoleName, "Default administrator", permissionValues.ToArray());
                await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

                await CreateUserAsync("admin", "admin", "Inbuilt Administrator", "admin@admin.com", "123456789", new string[] { adminRoleName });
                await CreateUserAsync("user", "user", "Inbuilt Standard User", "user@user.com", "123456789", new string[] { userRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }



            async Task EnsureRoleAsync(string roleName, string description, string[] claims)
            {
                if ((await _accountService.GetRoleByNameAsync(roleName)) == null)
                {
                    ApplicationRole applicationRole = new ApplicationRole(roleName, description);

                    var result = await this._accountService.CreateRoleAsync(applicationRole, claims);

                    if (!result.Item1)
                        throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
                }
            }

            async Task<ApplicationUser> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = userName,
                    FullName = fullName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    EmailConfirmed = true,
                    IsEnabled = true
                };

                var result = await _accountService.CreateUserAsync(applicationUser, roles, password);

                if (!result.Item1)
                    throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");


                return applicationUser;
            }
        }
    }
}
