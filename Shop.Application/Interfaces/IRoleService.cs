using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
    public interface IRoleService
    {
        public Task<ActionResult<ApplicationRole>> AddRole(string roleNam);
        public Task<ActionResult<ApplicationUserRole>> RemoveUserFromRole(string userName, string roleName);
        public Task<ActionResult<ApplicationUserRole>> AddRoleToUser(string userName, string roleName);

    }
}
