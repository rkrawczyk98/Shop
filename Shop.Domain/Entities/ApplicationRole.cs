using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class ApplicationRole : BaseEntity
    {
        public string Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
