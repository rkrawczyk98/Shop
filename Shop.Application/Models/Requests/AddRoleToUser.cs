﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Models.Requests
{
    public class AddRoleToUser
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
