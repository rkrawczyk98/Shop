using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Shop.UsersApi.Models;

namespace Shop.UsersApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
        public async Task<IActionResult> LoginAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(string email, string password, string firstName, string lastName)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                NormalizedEmail = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(string roleName)
        {
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
                    return Ok();
                }
                return BadRequest(result.Errors);

            }
            return BadRequest();
        }

        [HttpPost("removeUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string userName, string roleName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            IdentityRole role = await _roleManager.FindByNameAsync(roleName);
            if (role != null && user != null)
            {
                if(!await _userManager.IsInRoleAsync(user, roleName))
                {
                    return BadRequest();
                }
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return BadRequest();
        }

        [HttpPost("addRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string userName, string roleName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            IdentityRole role = await _roleManager.FindByNameAsync(roleName);
            if (role != null && user != null && !await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return BadRequest();
        }
    }
}
