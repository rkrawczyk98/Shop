using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccesFailedCout { get; set; }
    }
}
