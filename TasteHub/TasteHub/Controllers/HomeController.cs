using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TasteHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(UserManager<IdentityUser> _userManager)
        {
            userManager = _userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false) 
            {
                var user = await userManager.FindByIdAsync(User.Id());

                if (await userManager.IsInRoleAsync(user, "Admin")) 
                {
                    return RedirectToAction("AllUsers", "Home", new { area = "Admin"});
                }
            }
            return RedirectToAction("AllRecipes", "Recipe", new { area = "User",category = "all" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Admin() 
        {
            return RedirectToAction("AllUsers", "Home", new { area = "Admin" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }
            if (statusCode == 401)
            {
                return View("Error401");
            }
            if (statusCode == 404)
            {
                return View("Error404");
            }

            return View();
        }
    }
}
