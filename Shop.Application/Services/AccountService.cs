using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Newtonsoft.Json;
using Shop.Application.Interfaces;
//using Shop.Domain.Core.Models;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly string _productServiceBaseUrl;

        public AccountService()
        {
            _httpClient = new HttpClient();
            _productServiceBaseUrl = "http://localhost:5170";
        }

        public Task<bool> CheckPasswordAsync(Domain.Core.Models.ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> CreateRoleAsync(Domain.Core.Models.ApplicationRole role, IEnumerable<string> claims)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> CreateUserAsync(Domain.Core.Models.ApplicationUser user, IEnumerable<string> roles, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> DeleteRoleAsync(Domain.Core.Models.ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> DeleteRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> DeleteUserAsync(Domain.Core.Models.ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Core.Models.ApplicationRole> GetRoleByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Core.Models.ApplicationRole> GetRoleByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Core.Models.ApplicationRole> GetRoleLoadRelatedAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Domain.Core.Models.ApplicationRole>> GetRolesLoadRelatedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<Domain.Core.Models.ApplicationUser, string[]>> GetUserAndRolesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Core.Models.ApplicationUser> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Core.Models.ApplicationUser> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Core.Models.ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetUserRolesAsync(Domain.Core.Models.ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<Tuple<Domain.Core.Models.ApplicationUser, string[]>>> GetUsersAndRolesAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            var url = $"{_productServiceBaseUrl}/login";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SignInResult>(responseData);
            }

            return null;
        }

        public async Task<IdentityResult> RegisterUserAsync(string email, string password)
        {
            var url = $"{_productServiceBaseUrl}/register";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IdentityResult>(responseData);
            }

            return null;
        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string newPassword, string token)
        {
            var url = $"{_productServiceBaseUrl}/resetPassword";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IdentityResult>(responseData);
            }

            return null;
        }

        public Task<Tuple<bool, string[]>> ResetPasswordAsync(Domain.Core.Models.ApplicationUser user, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> UpdatePasswordAsync(Domain.Core.Models.ApplicationUser user, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> UpdateRoleAsync(ApplicationRole role, IEnumerable<string> claims)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> UpdateRoleAsync(Domain.Core.Models.ApplicationRole role, IEnumerable<string> claims)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> UpdateUserAsync(Domain.Core.Models.ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<bool, string[]>> UpdateUserAsync(Domain.Core.Models.ApplicationUser user, IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }
    }
}
