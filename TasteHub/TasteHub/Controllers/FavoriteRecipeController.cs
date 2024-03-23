using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;

namespace TasteHub.Controllers
{
    public class FavoriteRecipeController : BaseController
    {
        private readonly IFavoriteRecipeService favoriteRecipeService;
        private readonly IRecipeService recipeService;

        public FavoriteRecipeController(
            IFavoriteRecipeService _favoriteRecipeService, 
            IRecipeService _recipeService)
        {
            favoriteRecipeService = _favoriteRecipeService;
            recipeService = _recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> MyFavoriteRecipes()
        {
            var model = await favoriteRecipeService.GetAllFavoriteRecipesForUserAsync(User.Id());

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddFavoriteRecipe(int id) 
        {
            var recipes = await favoriteRecipeService.GetAllFavoriteRecipesAsync();

            if (recipes.Any(x => x.RecipeId == id)) 
            {
                return BadRequest();
            }

            var recipe = await recipeService.GetByIdAsync(id);

            if (recipe == null) 
            {
                return NotFound();
            }

            await favoriteRecipeService.AddAsync(new FavoriteRecipeInfoModel 
            (
                User.Id(),
                id,
                recipe
            ));

            return RedirectToAction("Details","Recipe", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFavoriteRecipe(int id) 
        {
            var recipes = await favoriteRecipeService.GetAllFavoriteRecipesAsync();

            var recipe = recipes.FirstOrDefault(x => x.UserId == User.Id() && x.RecipeId == id);

            if (recipe == null) 
            {
                return BadRequest();
            }

            await favoriteRecipeService.RemoveAsync(id, User.Id());

            return RedirectToAction(nameof(MyFavoriteRecipes));
        }
    }
}
