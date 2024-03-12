using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;

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

        [HttpGet]
        public async Task<IActionResult> AddRating(int recipeId) 
        {
            var recipe = await recipeService.GetByIdAsync(recipeId);

            if(recipe == null)
            {
                return BadRequest();
            }

            var model = new RatingFormModel()
            {
                RecipeId = recipeId,
                Value = 0.00
            };

            return View(model);
        }
    }
}
