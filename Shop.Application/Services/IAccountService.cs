using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(string email, string password);
        Task<SignInResult> LoginAsync(string email, string password);
        Task<IdentityResult> ResetPasswordAsync(string email, string newPassword, string token);
    }
}
