using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Admin;

namespace TasteHub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAdminService adminService;
        private readonly IRecipeService recipeService;

        public AdminController(
            IAdminService _adminService,
            IRecipeService _recipeService,
            RoleManager<IdentityRole> _roleManager,
            UserManager<IdentityUser> _userManager)
        {
            adminService = _adminService;
            recipeService = _recipeService;
            roleManager = _roleManager;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            var model = new RoleFormModel();
            return View(model);
           
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleFormModel model) 
        {
            if (ModelState.IsValid == false) 
            {
                return BadRequest();
            }

            if (await roleManager.RoleExistsAsync(model.Name) == false)
            {
                var role = new IdentityRole()
                {
                    Name = model.Name,
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

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var model = await adminService.GetAllUsersAsync();

            foreach (var user in model)
            {
                user.RecipesCount = await recipeService.GetRecipesCountByUsernameAsync(user.Id);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllQueries() 
        {
            var model = await adminService.GetAllQueriesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllRoles() 
        {
            var model = await adminService.GetAllRolesAsync();

            return View(model);
        }
    }
}
