using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shop.Application.Services;
using Shop.Domain.Entities;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace ApiDoTestow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KontrollerDoTestow : ControllerBase
    {
        private readonly HttpClient _client;
        public KontrollerDoTestow(HttpClient httpClient) 
        {
            _client = httpClient;
        }


        [HttpPost("addRole")]
        public async Task<ActionResult<ApplicationRole>> TworzenieRoli(string roleName) // To git
        {
            try
            {
                var data = new JObject { ["roleName"] = roleName };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/auth/addRole", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var role = JsonConvert.DeserializeObject<ApplicationRole>(responseContent);

                return Ok(role);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }


        [HttpPut("addRoleToUser")] 
        public async Task<ActionResult<ApplicationUser>> DodawanieRoliDoUzytkownika(string userName, string roleName) //byæ mo¿e ta metoda bêdzie mia³a zwracaæ coœ innego
        {
            try
            {
                var data = new JObject { ["userName"] = userName,["roleName"] = roleName };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync("http://localhost:5170/api/auth/addRoleToUser", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(responseContent);

                return Ok(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpDelete("removeUserFromRole")]
        public async Task<ActionResult> UsuwanieUzytkownikaZRoli(string userName, string roleName) //byæ mo¿e ta metoda bêdzie mia³a zwracaæ coœ innego
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:5170/api/auth/removeUserFromRole?userName={userName}&roleName={roleName}");

                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}