using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Shop.Domain.Entities;
using Shop.Application.Services;

namespace Shop.WebUi.Controllers
{
    public class RoleController : Controller
    {
        private RoleService roleManager; //private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public RoleController(/*RoleManager<IdentityRole> roleMgr*/ RoleService roleMgr, UserManager<ApplicationUser> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Index() => View(roleManager.Roles);

        public bool RoleExists(string name)
        {
            foreach (var role in roleManager.Roles)
            {
                if (role.ToString() == name) return true;
            }
            return false;
        }

        //private void Errors(ApplicationRole result)
        //{
        //    foreach (ApplicationRole error in result.Errors)
        //        ModelState.AddModelError("", error.Description);
        //}

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Required] string name) //
        {
            if (ModelState.IsValid)
            {
                
                ActionResult<ApplicationRole> result =  await roleManager.AddRole(name); /*await roleManager.CreateAsync(new IdentityRole(name));*/
                if (result.Value != null)
                    return RedirectToAction("Index");
                else
                    throw new NotImplementedException();//await Errors(result);
            }
            return View(name);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            ActionResult<ApplicationRole> resultRole = await roleManager.FindRoleById(id);
            ApplicationRole role = resultRole.Value;
            if (role != null)
            {
                ActionResult<ApplicationRole> result = await roleManager.RemoveRole(role);
                //ApplicationRole result = pickedRole.Value;
                if (result.Value != null)
                    return RedirectToAction("Index");
                else
                    throw new NotImplementedException();//Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id)
        {
            ActionResult<ApplicationRole> result = await roleManager.UpdateRole(id);
            ApplicationRole role = result.Value;
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            foreach (ApplicationUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

    }
}
