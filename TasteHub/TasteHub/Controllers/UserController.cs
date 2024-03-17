using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace TasteHub.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(
            UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName) == false)
            {
                var role = new IdentityRole()
                {
                    Name = roleName,
                };

                await roleManager.CreateAsync(role);
            }

            return RedirectToAction("AllRecipes", "Recipe");
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string username, string roleName) 
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                var user = await userManager.FindByNameAsync(username);

                if (user != null)
                {
                    if (await userManager.IsInRoleAsync(user, roleName) == false)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }

            return RedirectToAction("AllRecipes", "Recipe");
        }
    }
}
