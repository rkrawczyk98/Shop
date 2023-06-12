using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient httpClient) 
        {
            _client = httpClient;
        }

        public async Task<ApplicationUser> LoginAsync(string userName, string password)
        {
            HttpResponseMessage response = await _client.GetAsync("http://localhost:5170/api/auth/login");
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> RegisterUserAsync(string email, string password, string firstName, string lastName)
        {
            HttpResponseMessage response = await _client.GetAsync("http://localhost:5170/api/auth/register");
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> ResetUserPasswordAsync(string email, string newPassword)
        {
            HttpResponseMessage response = await _client.GetAsync("http://localhost:5170/api/auth/resetPassword");
            throw new NotImplementedException();
        }
    }
}
