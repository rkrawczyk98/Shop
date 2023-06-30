using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Shop.UsersApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Shop.UsersApi.Controllers
{

    [ApiController]
    [Route("api/auth")]
    //[Authorize]
    public class AccountServiceController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountServiceController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    var data = JObject.Parse(requestBody);
                    var userName = data.Value<string>("userName");
                    var password = data.Value<string>("password");

                    var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    return BadRequest(result.IsNotAllowed);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> RegisterUserAsync()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    var data = JObject.Parse(requestBody);
                    var userName = data.Value<string>("userName");
                    var password = data.Value<string> ("password");
                    var email = data.Value<string>("email");
                    var firstName = data.Value<string>("firstName");
                    var lastName = data.Value<string>("lastName");

                    if (await _userManager.FindByEmailAsync(email) == null)
                    {
                        var newUser = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = userName,
                            Email = email,
                            FirstName = firstName,
                            LastName = lastName,
                            NormalizedEmail = email
                        };
                        var result = await _userManager.CreateAsync(newUser,password);
                        var createdUser = await _userManager.FindByEmailAsync(email);
                        if (result.Succeeded)
                        {
                            return Ok(createdUser);
                        }

                        var errors = result.Errors.Select(e => new { e.Code, e.Description });
                        return BadRequest(new ProblemDetails { Title = "Failed to create user", Detail = "User creation failed.", Status = 400, Extensions = { ["errors"] = errors } });
                    }
                    else
                    {
                        return BadRequest("User with this e-mail already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult> ResetPasswordAsync()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    var data = JObject.Parse(requestBody);
                    var email = data.Value<string>("email");
                    var newPassword = data.Value<string>("newPassword");

                    var user = await _userManager.FindByEmailAsync(email);
                    if (user != null)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                        if (result.Succeeded)
                        {
                            return Ok();
                        }
                        var errors = result.Errors.Select(e => new { e.Code, e.Description });
                        return BadRequest(new ProblemDetails { Title = "Failed to change password", Detail = "Password changing failed.", Status = 400, Extensions = { ["errors"] = errors } });
                    }
                    else
                    {
                        return BadRequest("User not exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }


        [HttpPost("addRole")]
        public async Task<ActionResult<IdentityRole>> AddRole()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    var data = JObject.Parse(requestBody);
                    var roleName = data.Value<string>("roleName");

                    if (await _roleManager.FindByNameAsync(roleName) == null)
                    {
                        var newRole = new IdentityRole
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = roleName
                        };
                        var result = await _roleManager.CreateAsync(newRole);
                        if (result.Succeeded)
                        {
                            return CreatedAtAction(nameof(AddRole), newRole);
                        }

                        var errors = result.Errors.Select(e => new { e.Code, e.Description });
                        return BadRequest(new ProblemDetails { Title = "Failed to create role", Detail = "Role creation failed.", Status = 400, Extensions = { ["errors"] = errors } });
                    }
                    else
                    {
                        return BadRequest("Role already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpDelete("removeUserFromRole")]
        public async Task<ActionResult> RemoveUserFromRole(string userName, string roleName) //być może ta metoda będzie miała zwracać coś innego
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(userName);
                IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                if (role != null && user != null)
                {
                    if (!await _userManager.IsInRoleAsync(user, roleName))
                    {
                        return BadRequest();
                    }
                    var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    var errors = result.Errors.Select(e => new { e.Code, e.Description });
                    return BadRequest(new ProblemDetails { Title = "Failed to delete role", Detail = "Deleting role failed.", Status = 400, Extensions = { ["errors"] = errors } });
                }
                return BadRequest();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPut("addRoleToUser")]
        public async Task<ActionResult<IdentityUserRole<string>>> AddRoleToUser() //być może ta metoda będzie miała zwracać coś innego
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    var data = JObject.Parse(requestBody);
                    var userName = data.Value<string>("userName");
                    var roleName = data.Value<string>("roleName");

                    ApplicationUser user = await _userManager.FindByNameAsync(userName);
                    IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null && user != null && !await _userManager.IsInRoleAsync(user, roleName))
                    {
                        var result = await _userManager.AddToRoleAsync(user, roleName);
                        if (result.Succeeded)
                        {
                            return CreatedAtAction(nameof(AddRoleToUser), user);
                        }
                        var errors = result.Errors.Select(e => new { e.Code, e.Description });
                        return BadRequest(new ProblemDetails { Title = "Failed to create role", Detail = "Role creation failed.", Status = 400, Extensions = { ["errors"] = errors } });
                    }
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

        }

        [HttpGet("roles")]
        public async Task<ActionResult<IQueryable<IdentityRole>>> Roles()
        {
            try
            {
                return Ok(_roleManager.Roles);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpGet("users")]
        public async Task<ActionResult<IQueryable<IdentityUser>>> Users()
        {
            try
            {
                return Ok(_userManager.Users);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpDelete("removeUser")]
        public async Task<ActionResult> RemoveUser(string userName)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    var errors = result.Errors.Select(e => new { e.Code, e.Description });
                    return BadRequest(new ProblemDetails { Title = "Failed to delete user", Detail = "Deleting user failed.", Status = 400, Extensions = { ["errors"] = errors } });
                }
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
                IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    var errors = result.Errors.Select(e => new { e.Code, e.Description });
                    return BadRequest(new ProblemDetails { Title = "Failed to delete role", Detail = "Deleting role failed.", Status = 400, Extensions = { ["errors"] = errors } });
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
