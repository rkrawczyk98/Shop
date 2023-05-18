using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Newtonsoft.Json;
using Shop.Application.Interfaces;
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
    }
}
