using Microsoft.AspNetCore.Identity;

namespace Shop.UsersApi.Models
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public ApplicationUserLogin() 
        {
            this.User = new ApplicationUser();
        }
        public virtual ApplicationUser User { get; set; }
    }
}
