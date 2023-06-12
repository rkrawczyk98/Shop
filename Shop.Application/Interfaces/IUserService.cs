using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
    public interface IUserService
    {
        public Task<ApplicationUser> LoginAsync(string userName,string password);
        public Task<ApplicationUser> RegisterUserAsync(string email, string password, string firstName, string lastName);
        public Task<ApplicationUser> ResetUserPasswordAsync(string email, string newPassword);
    }
}
