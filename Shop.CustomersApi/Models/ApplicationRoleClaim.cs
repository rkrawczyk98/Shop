using Microsoft.AspNetCore.Identity;

namespace Shop.UsersApi.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public ApplicationRoleClaim() 
        {
            this.Role = new ApplicationRole();
        }
        public virtual ApplicationRole Role { get; set; }
    }
}
