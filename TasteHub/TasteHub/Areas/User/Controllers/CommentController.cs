using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Comment;

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
            var recipe = await recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                return BadRequest();
            }

            var model = await commentService.GetAllCommentsAboutRecipeAsync(id);

            if (!model.Any())
            {
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

            var recipe = await recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.CreatorId == User.Id())
            {
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

            var recipe = await recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (recipe.CreatorId == User.Id())
            {
                return Unauthorized();
            }

            await commentService.AddAsync(model);

            return RedirectToAction(nameof(GetAllComments), new { id });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await commentService.GetByIdAsync(id);

            if (comment == null)
            {
                return BadRequest();
            }

            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            await commentService.DeleteAsync(id);

            return RedirectToAction(nameof(GetAllComments), new { id = comment.RecipeId });
        }
    }
}
