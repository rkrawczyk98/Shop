using Microsoft.AspNetCore.Identity;

namespace Shop.UsersApi.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUserRole() 
        {
            this.Role = new ApplicationRole();
            this.User= new ApplicationUser();
        }
        public virtual ApplicationRole Role { get; set; } 
        public virtual ApplicationUser User { get; set; }
    }
}
