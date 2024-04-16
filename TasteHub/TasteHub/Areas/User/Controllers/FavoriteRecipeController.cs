using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Core.Models.Recipe;

namespace TasteHub.Areas.User.Controllers
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

            if (recipes.Any(x => x.RecipeId == id && x.UserId == User.Id()))
            {
                TempData["Danger"] = $"You have already added this recipe to your favorites!";
                return BadRequest();
            }

            RecipeInfoViewModel? recipe;

            try
            {
                recipe = await recipeService.GetByIdAsync(id);
            }
            catch (Exception)
            {
                TempData["Danger"] = $"This recipe doesn't exist!";
                return BadRequest();
            }

            await favoriteRecipeService.AddAsync(new FavoriteRecipeInfoModel
            (
                User.Id(),
                id,
                recipe
            ));
            TempData["Successful"] = $"You have successfully added a {recipe.Title} to your favorite recipes!";

            return RedirectToAction("Details", "Recipe", new { area="User", id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFavoriteRecipe(int id)
        {
            var recipes = await favoriteRecipeService.GetAllFavoriteRecipesAsync();

            var recipe = recipes.FirstOrDefault(x => x.UserId == User.Id() && x.RecipeId == id);

            if (recipe == null)
            {
                TempData["Danger"] = $"This recipe doesn't exist!";
                return BadRequest();
            }

            await favoriteRecipeService.DeleteAsync(id, User.Id());
            TempData["Successful"] = $"You have successfully removed a recipe from your favorite recipes!";

            return RedirectToAction(nameof(MyFavoriteRecipes));
        }
    }
}
