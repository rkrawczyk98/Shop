using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shop.Application.Services;
using Shop.Domain.Entities;
using System.Data;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

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

                if (response.IsSuccessStatusCode)
                    return Ok(role);
                else
                    return BadRequest();
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

                if (response.IsSuccessStatusCode)
                    return Ok(user);
                else
                    return BadRequest();
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

                if(response.IsSuccessStatusCode)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(string userName, string password)
        {
            try
            {
                var data = new JObject { ["userName"] = userName, ["password"] = password };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/auth/login", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                //var user = JsonConvert.DeserializeObject<ApplicationUser>(responseContent);

                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("registerUser")]
        public async Task<ActionResult<ApplicationUser>> RegisterUserAsync(string userName, string email, string password, string firstName, string lastName)
        {
            try
            {
                var data = new JObject { ["userName"] = userName, ["email"] = email, ["password"] = password, ["firstName"] = firstName, ["lastName"] = lastName };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/auth/register", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ApplicationUser>(responseContent);

                if (response.IsSuccessStatusCode)
                    return Ok(user);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult> ResetUserPasswordAsync(string email, string newPassword)
        {
            try
            {
                var data = new JObject { ["email"] = email, ["newPassword"] = newPassword };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/auth/resetPassword", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                //var user = JsonConvert.DeserializeObject<ApplicationUser>(responseContent);

                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpGet("allRoles")]
        public async Task<ActionResult<IQueryable<IdentityRole>>> GetAllRoles()
        {
            HttpResponseMessage response = await _client.GetAsync("http://localhost:5170/api/auth/roles");
            var responseContent = await response.Content.ReadAsStringAsync();
            var rolesList = JsonConvert.DeserializeObject<List<IdentityRole>>(responseContent);
            var roles = rolesList.AsQueryable();

            if (response.IsSuccessStatusCode)
                return Ok(roles);
            return BadRequest();
        }

        [HttpGet("allUsers")]
        public async Task<ActionResult<IQueryable<IdentityUser>>> GetAllUsers()
        {
            HttpResponseMessage response = await _client.GetAsync("http://localhost:5170/api/auth/users");
            var responseContent = await response.Content.ReadAsStringAsync();
            var usersList = JsonConvert.DeserializeObject<List<IdentityUser>>(responseContent);
            var users = usersList.AsQueryable();

            if (response.IsSuccessStatusCode)
                return Ok(users);
            return BadRequest();
        }

        [HttpDelete("removeUser")]
        public async Task<ActionResult> RemoveUser(string userName)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:5170/api/auth/removeUser?userName={userName}");

                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpDelete("removeRole")]
        public async Task<ActionResult> RemoveRole(string roleName)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:5170/api/auth/removeRole?roleName={roleName}");

                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("createCategory")]
        public async Task<ActionResult<Category>> CreateCategory(string categoryName, string? description)
        {
            try
            {
                var data = new JObject();
                data["Name"] = categoryName;
                data["Description"] = description;
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5044/api/categories/CreateCategory", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<Category>(responseContent);

                if (response.IsSuccessStatusCode)
                    return Ok(category);
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpGet("getCategory")]
        public async Task<ActionResult<Category>> GetCategory(string categoryName)
        {
            try
            {
                //var data = new JObject();
                //data["categoryName"] = categoryName;
                //var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.GetAsync($"http://localhost:5044/api/categories/categoryName?categoryName={categoryName}");
                var responseContent = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<Category>(responseContent);

                if (response.IsSuccessStatusCode)
                    return Ok(category);
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpGet("getAllCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            try
            {
                //var data = new JObject();
                //data["categoryName"] = categoryName;
                //var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.GetAsync($"http://localhost:5044/api/categories/GetAllCategories");
                var responseContent = await response.Content.ReadAsStringAsync();
                var categoriesAll = JsonConvert.DeserializeObject<List<Category>>(responseContent);
                var categories = categoriesAll.AsQueryable();

                if (response.IsSuccessStatusCode)
                    return Ok(categories);
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPut("updateCategory")]
        public async Task<ActionResult<Category>> UpdateCategory(string categoryName, string newDesciption)
        {
            try
            {
                var data = new JObject { ["categoryName"] = categoryName, ["newDesciption"] = newDesciption };

                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync($"http://localhost:5044/api/categories/Update/categoryName?categoryName={categoryName}&newDesciption={newDesciption}", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var resultCategory = JsonConvert.DeserializeObject<Category>(responseContent);

                if (response.IsSuccessStatusCode)
                    return Ok(resultCategory);
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpDelete("removeCategory")]
        public async Task<ActionResult> DeleteCategory(string categoryName)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:5044/api/categories/Delete/categoryName?categoryName={categoryName}");

                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }


        [HttpPost("createProduct")]
        public async Task<ActionResult<Product>> CreateProduct(string name, string description,string categoryName, decimal price, int stock)
        {
            try
            {
                var data = new JObject();
                data["Name"] = name;
                data["Description"] = description;
                data["CategoryName"] = categoryName;
                data["Price"] = price;
                data["Stock"] = stock;
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5044/api/products/createProduct", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(responseContent);

                if (response.IsSuccessStatusCode)
                    return Ok(product);
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpGet("getProductById")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                //var data = new JObject();
                //data["id"] = id;
                //var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.GetAsync($"http://localhost:5044/api/products/getProductById?id={id}");
                var responseContent = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(responseContent);

                if (response.IsSuccessStatusCode)
                    return Ok(product);
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpGet("getAllProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            HttpResponseMessage response = await _client.GetAsync($"http://localhost:5044/api/products/getAllProducts");
            var responseContent = await response.Content.ReadAsStringAsync();
            var productsAll = JsonConvert.DeserializeObject<List<Category>>(responseContent);
            var products = productsAll.AsEnumerable();

            if (response.IsSuccessStatusCode)
                return Ok(products);
            else
                return BadRequest();
        }

        [HttpDelete("deleteProduct")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"http://localhost:5044/api/products/deleteProduct?id={id}");
                if (response.IsSuccessStatusCode)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        //[HttpPost("CreateOrder")]
        //public async Task<ActionResult<Order>> CreateOrder(Order order)
        //{
        //    try
        //    {
        //        var data = new JObject();
        //        data["Name"] = name;
        //        data["Description"] = description;
        //        data["CategoryName"] = categoryName;
        //        data["Price"] = price;
        //        data["Stock"] = stock;
        //        var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _client.PostAsync("http://localhost:5035/api/orders/CreateOrder", contentdata);
        //        var responseContent = await response.Content.ReadAsStringAsync();
        //        var product = JsonConvert.DeserializeObject<Product>(responseContent);

        //        if (response.IsSuccessStatusCode)
        //            return Ok(product);
        //        else
        //            return BadRequest();

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        return null;
        //    }
        //}
    }
}