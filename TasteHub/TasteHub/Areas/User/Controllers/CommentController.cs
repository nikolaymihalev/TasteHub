using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Comment;
using TasteHub.Core.Models.Recipe;

namespace TasteHub.Areas.User.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly IRecipeService recipeService;

        public CommentController(
            ICommentService _commentService,
            IRecipeService _recipeService)
        {
            commentService = _commentService;
            recipeService = _recipeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllComments(int id)
        {
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

            var model = await commentService.GetAllCommentsAboutRecipeAsync(id);

            if (!model.Any())
            {
                TempData["Danger"] = $"This comment doesn't exist!";
                return BadRequest();
            }

            foreach (var view in model)
            {
                view.RecipeTitle = recipe.Title;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddComment(int id)
        {
            var model = new CommentFormModel();

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

            if (recipe.CreatorId == User.Id())
            {
                TempData["Danger"] = $"You do not have permission to perform this operation!";
                return Unauthorized();
            }

            model.RecipeId = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentFormModel model, int id)
        {
            model.RecipeId = id;
            model.UserId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(model);
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

            if (recipe.CreatorId == User.Id())
            {
                TempData["Danger"] = $"You do not have permission to perform this operation!";
                return Unauthorized();
            }

            await commentService.AddAsync(model);

            TempData["Successful"] = $"You have successfully added a comment!";
            return RedirectToAction(nameof(GetAllComments), new { id });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            CommentInfoModel? comment;

            try
            {
                comment = await commentService.GetByIdAsync(id);

            }
            catch (Exception)
            {
                TempData["Danger"] = $"This comment doesn't exist!";
                return BadRequest();

            }

            if (!User.IsInRole("Admin"))
            {
                TempData["Danger"] = $"You do not have permission to perform this operation!";
                return Unauthorized();
            }

            await commentService.DeleteAsync(id);
            TempData["Successful"] = $"You have successfully deleted a comment!";

            return RedirectToAction(nameof(GetAllComments), new { id = comment.RecipeId });
        }
    }
}
