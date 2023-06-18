using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Shop.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _client;

        public RoleService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<ActionResult<ApplicationRole>> AddRole(string roleName)
        {
            try
            {
                var data = new JObject { ["roleName"] = roleName };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/auth/addRole", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var role = JsonConvert.DeserializeObject<ApplicationRole>(responseContent);

                return role;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ActionResult<ApplicationUser>> AddRoleToUser(string userName, string roleName)
        {
            try
            {
                var data = new JObject { ["userName"] = userName, ["roleName"] = roleName };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync("http://localhost:5170/api/auth/addRoleToUser", contentdata);
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

        public async Task<ActionResult<ApplicationRole>> RemoveUserFromRole(string userName, string roleName)
        {
            HttpResponseMessage response = await _client.GetAsync("http://localhost:5170/api/auth/removeUserFromRole");
            throw new NotImplementedException();
        }
    }
}
