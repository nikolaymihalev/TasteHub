using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;

namespace TasteHub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserService userService;
        private readonly IRecipeService recipeService;

        public AdminController(
            IUserService _userService,
            IRecipeService _recipeService,
            RoleManager<IdentityRole> _roleManager,
            UserManager<IdentityUser> _userManager)
        {
            userService = _userService;
            recipeService = _recipeService;
            roleManager = _roleManager;
            userManager = _userManager;
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

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            var model = await userService.GetAllUsersAsync();

            foreach (var user in model)
            {
                user.RecipesCount = await recipeService.GetRecipesCountByUsernameAsync(user.Id);
            }

            return View(model);
        }
    }
}
