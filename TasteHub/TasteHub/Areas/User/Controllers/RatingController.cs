using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Rating;
using TasteHub.Core.Models.Recipe;

namespace TasteHub.Areas.User.Controllers
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
            RecipeInfoViewModel? recipe;

            try
            {
                recipe = await recipeService.GetByIdAsync(recipeId);
            }
            catch (Exception)
            {
                TempData["Danger"] = $"This recipe doesn't exist!";
                return BadRequest();
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
            RecipeInfoViewModel? recipe;

            try
            {
                recipe = await recipeService.GetByIdAsync(recipeId);
            }
            catch (Exception)
            {
                TempData["Danger"] = $"This recipe doesn't exist!";
                return BadRequest();
            }

            if (recipe.CreatorId == User.Id())
            {
                TempData["Danger"] = $"You do not have permission to perform this operation!";
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

            RecipeInfoViewModel? recipe;

            try
            {
                recipe = await recipeService.GetByIdAsync(recipeId);
            }
            catch (Exception)
            {
                TempData["Danger"] = $"This recipe doesn't exist!";
                return BadRequest();
            }

            if (recipe.CreatorId == User.Id())
            {
                TempData["Danger"] = $"You do not have permission to perform this operation!";
                return Unauthorized();
            }

            var ratings = await ratingService.GetAllRatingsAboutRecipeAsync(recipeId);

            if (ratings.Any(x => x.UserId == User.Id()))
            {
                TempData["Danger"] = $"This user doesn't exist!";
                return BadRequest();
            }

            await ratingService.AddAsync(model);
            TempData["Successful"] = $"You have successfully added a rating to {recipe.Title}!";

            return RedirectToAction(nameof(GetAllRatings), new { recipeId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRating(int id, int recipeId, string userId)
        {
            var rating = await ratingService.GetAllRatingsAboutRecipeAsync(recipeId);

            if (rating.FirstOrDefault(x => x.UserId == userId) == null)
            {
                TempData["Danger"] = $"This user doesn't exist!";
                return BadRequest();
            }

            if (!User.IsInRole("Admin"))
            {
                TempData["Danger"] = $"You do not have permission to perform this operation!";
                return Unauthorized();
            }

            await ratingService.DeleteAsync(id);
            TempData["Successful"] = $"You have successfully removed a rating!";

            return RedirectToAction(nameof(GetAllRatings), new { recipeId });
        }

        [HttpGet]
        public async Task<IActionResult> EditRating(int recipeId)
        {
            var allRatings = await ratingService.GetAllRatingsAboutRecipeAsync(recipeId);

            if (!allRatings.Any())
            {
                TempData["Danger"] = $"This recipe doesn't exist!";
                return BadRequest();
            }

            var rating = allRatings.FirstOrDefault(x => x.UserId == User.Id() && x.RecipeId == recipeId);

            if (rating == null)
            {
                TempData["Danger"] = $"You have no rating for this recipe!";
                return BadRequest();
            }

            if (rating.UserId != User.Id())
            {
                TempData["Danger"] = $"You do not have permission to perform this operation!";
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
                TempData["Danger"] = $"This recipe doesn't exist!";
                return BadRequest();
            }

            var rating = allRatings.FirstOrDefault(x => x.UserId == User.Id() && x.RecipeId == recipeId);

            if (rating == null)
            {
                TempData["Danger"] = $"You have no rating for this recipe!";
                return BadRequest();
            }

            if (rating.UserId != User.Id())
            {
                TempData["Danger"] = $"You do not have permission to perform this operation!";
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
            TempData["Successful"] = $"You have successfully edited a rating!";

            return RedirectToAction(nameof(GetAllRatings), new { recipeId });
        }
    }
}
