using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                var data = new JObject { ["userName"] = userName , ["password"] = password};
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/auth/login", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(responseContent);

                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ApplicationUser> RegisterUserAsync(string email, string password, string firstName, string lastName)
        {
            try
            {
                var data = new JObject { ["email"] = email, ["password"] = password, ["firstName"] = firstName, ["lastName"] = lastName };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/auth/register", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(responseContent);

                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ApplicationUser> ResetUserPasswordAsync(string email, string newPassword)
        {
            try
            {
                var data = new JObject { ["email"] = email, ["newPassword"] = newPassword };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/auth/login", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(responseContent);

                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
