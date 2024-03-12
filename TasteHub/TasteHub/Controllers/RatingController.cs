using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Data.Models;

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
                return NotFound();
            }

            if (recipe.CreatorId == User.Id()) 
            {
                return Unauthorized();
            }

            var model = new RatingFormModel()
            {
                RecipeId = recipeId,
                Value = 0.00,
                UserId = User.Id()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(RatingFormModel model, int recipeId) 
        {
            model.UserId = User.Id();

            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var recipe = await recipeService.GetByIdAsync(recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.CreatorId == User.Id())
            {
                return Unauthorized();
            }

            var ratings = await ratingService.GetAllRatingsAboutRecipeAsync(recipeId);

            if (ratings.Any(x => x.UserId == User.Id())) 
            {
                return BadRequest();
            }

            await ratingService.AddAsync(model);

            return RedirectToAction(nameof(GetAllRatings), new { recipeId = recipeId });
        }
    }
}
