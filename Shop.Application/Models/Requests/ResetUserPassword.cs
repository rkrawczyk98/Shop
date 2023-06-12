using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Models.Requests
{
    public class ResetUserPassword
    {
        public string Email { get; set; }
        public string newPassword { get; set; }
    }
}
