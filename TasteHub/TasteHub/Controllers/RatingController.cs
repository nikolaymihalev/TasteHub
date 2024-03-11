using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;

namespace TasteHub.Controllers
{
    public class RatingController : BaseController
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService _ratingService)
        {
            ratingService = _ratingService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
