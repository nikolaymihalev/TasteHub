using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;

namespace TasteHub.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            commentService = _commentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllComments(int id) 
        {
            var model = await commentService.GetAllCommentsAboutRecipeAsync(id);

            return View(model);
        }

        [HttpGet]
        public IActionResult AddComment(int id) 
        {
            var model = new CommentFormModel();
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

            await commentService.AddSync(model);

            return RedirectToAction(nameof(GetAllComments), new { id = id });
        }
    }
}
