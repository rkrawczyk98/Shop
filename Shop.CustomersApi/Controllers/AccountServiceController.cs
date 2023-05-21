﻿using Microsoft.AspNetCore.Identity;
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

        public AccountServiceController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName,password,isPersistent: false,lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(string email, string password,string firstName, string lastName)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = email,
                Email = email,
                FirstName= firstName,
                LastName= lastName,
                Password = password,
                NormalizedEmail= email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string email,string newPassword)
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
        public async Task<IActionResult> AddRole(string userName, string password)
        {

        }
    }
}
