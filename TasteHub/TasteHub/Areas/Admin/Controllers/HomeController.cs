using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Admin;

namespace TasteHub.Areas.Admin.Controllers
{    
    public class HomeController : BaseAdminController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAdminService adminService;
        private readonly IRecipeService recipeService;

        public HomeController(
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

                TempData["Successful"] = $"You have successfully created a role with name {model.Name}!";

                await roleManager.CreateAsync(role);
            }

            return RedirectToAction(nameof(AllRoles));
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string username, string roleName, int queryId)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                var user = await userManager.FindByNameAsync(username);

                if (user != null)
                {
                    if (await userManager.IsInRoleAsync(user, roleName) == false)
                    {
                        await userManager.AddToRoleAsync(user, roleName);

                        if (await adminService.QueryExistsAsync(queryId))
                        {
                            await adminService.RemoveAsync(queryId);
                        }

                        TempData["Successful"] = $"You have successfully added {user.UserName} to {roleName}!";
                    }
                }
            }

            return RedirectToAction(nameof(AllUsers));
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var model = await adminService.GetAllUsersAsync();

            foreach (var user in model)
            {
                user.RecipesCount = await recipeService.GetRecipesCountByUserIdAsync(user.Id);
                var person = await userManager.FindByIdAsync(user.Id);
                var userRoles = await userManager.GetRolesAsync(person);
                user.RoleName = userRoles[0];
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

        [HttpGet]
        public async Task<IActionResult> RemoveQuery(int id)
        {
            if (await adminService.QueryExistsAsync(id) == false)
            {
                TempData["Danger"] = $"This query doesn't exist!";

                return BadRequest();
            }

            await adminService.RemoveAsync(id);

            TempData["Successful"] = $"You have successfully remove the query!";
            return RedirectToAction(nameof(AllQueries));
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var model = await recipeService.GetRecipeStatisticsAsync(); 
            return View(model);
        }
    }
}
