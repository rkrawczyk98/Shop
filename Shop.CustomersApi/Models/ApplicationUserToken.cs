using Microsoft.AspNetCore.Identity;

namespace Shop.UsersApi.Models
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public ApplicationUserToken()
        {
            this.User = new ApplicationUser();
        }
        public virtual ApplicationUser User { get; set; }
    }
}
