using Microsoft.AspNetCore.Identity;

namespace Shop.UsersApi.Models
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public ApplicationUserClaim() 
        {
            this.User = new ApplicationUser();
        }
        public virtual ApplicationUser User { get; set; }
    }
}
