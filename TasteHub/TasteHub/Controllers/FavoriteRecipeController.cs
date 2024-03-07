using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;

namespace TasteHub.Controllers
{
    public class FavoriteRecipeController : BaseController
    {
        private readonly IFavoriteRecipeService favoriteRecipeService;

        public FavoriteRecipeController(IFavoriteRecipeService _favoriteRecipeService)
        {
            favoriteRecipeService = _favoriteRecipeService;
        }

        [HttpGet]
        public async Task<IActionResult> MyFavoriteRecipes()
        {
            var allRecipes = await favoriteRecipeService.GetAllFavoriteRecipesAsync();
            var model = allRecipes.Where(x => x.CreatorId == User.Id());

            return View(model);
        }
    }
}
