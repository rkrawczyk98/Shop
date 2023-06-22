using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Models.Requests
{
    public class CreateUser
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength (25)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string NormalizedEmail { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
