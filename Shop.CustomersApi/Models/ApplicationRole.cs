using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.UsersApi.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole() : base()
        {
            this.UserRoles= new HashSet<ApplicationUserRole>();
            this.RoleClaims= new HashSet<ApplicationRoleClaim>();
        }

        public string? Description { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }

    }
}
