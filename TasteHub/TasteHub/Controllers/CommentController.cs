using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;

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
    }
}
