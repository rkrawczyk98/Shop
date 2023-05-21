//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.UsersApi.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser() : base()
        {
            this.Roles = new HashSet<ApplicationUserRole>();
            this.Claims = new HashSet<ApplicationUserClaim>();
            this.Logins = new HashSet<ApplicationUserLogin>();
            this.Tokens = new HashSet<ApplicationUserToken>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }
        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserRole> Roles { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }  
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
    }
}
