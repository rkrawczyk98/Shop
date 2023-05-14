using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Shop.Domain.Entities;
using Shop.Application.Services;

namespace Shop.UsersApi.Controllers
{
    public class AccountServiceController : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public async Task<SignInResult> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            return result;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
                // Set additional user properties if needed
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            return result;
        }
    }
}
