using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

            if (model.Count() == 0) 
            {
                return RedirectToAction("Details", "Recipe", new { id = recipeId });
            }

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
                Value = 0,
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRating(int recipeId,string userId)
        {
            var rating = await ratingService.GetAllRatingsAboutRecipeAsync(recipeId);

            if (rating.FirstOrDefault(x => x.UserId == userId) == null) 
            {
                return BadRequest();
            }

            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            await ratingService.DeleteAsync(recipeId,userId);

            return RedirectToAction(nameof(GetAllRatings), new { recipeId = recipeId });
        }

        [HttpGet]
        public async Task<IActionResult> EditRating(int recipeId) 
        {
            var allRatings = await ratingService.GetAllRatingsAboutRecipeAsync(recipeId);

            if (!allRatings.Any()) 
            {
                return BadRequest();
            }

            var rating = allRatings.FirstOrDefault(x => x.UserId == User.Id() && x.RecipeId == recipeId);

            if (rating == null)
            {
                return BadRequest();
            }

            if (rating.UserId != User.Id()) 
            {
                return Unauthorized();
            }

            var model = new RatingFormModel()
            {
                RecipeId = recipeId,
                UserId = User.Id(),
                Value = (int)rating.Value
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRating(RatingFormModel model, int recipeId)
        {
            var allRatings = await ratingService.GetAllRatingsAboutRecipeAsync(recipeId);

            if (!allRatings.Any())
            {
                return BadRequest();
            }

            var rating = allRatings.FirstOrDefault(x => x.UserId == User.Id() && x.RecipeId == recipeId);

            if (rating == null)
            {
                return BadRequest();
            }

            if (rating.UserId != User.Id())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid) 
            {
                model.UserId = User.Id();
                model.RecipeId = recipeId;
                return View(model);
            }

            model.UserId = User.Id();
            await ratingService.EditAsync(model);

            return RedirectToAction(nameof(GetAllRatings), new { recipeId = recipeId });
        }
    }
}
