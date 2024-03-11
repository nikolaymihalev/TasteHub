using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;

namespace TasteHub.Controllers
{
    public class RatingController : BaseController
    {
        private readonly IRatingService ratingService;
        private readonly IRecipeService recipeService;

        public RatingController(
            IRatingService _ratingService, 
            IRecipeService _recipeService)
        {
            ratingService = _ratingService;
            recipeService = _recipeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRatings(int recipeId)
        {
            var recipe = await recipeService.GetByIdAsync(recipeId);

            if (recipe == null) 
            {
                return NotFound();
            }

            var model = await ratingService.GetAllRatingsAboutRecipeAsync(recipeId);

            model.All(x => x.RecipeTitle == recipe.Title);

            return View(model);
        }
    }
}
