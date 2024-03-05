using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;

namespace TasteHub.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;   
        }

        [HttpGet]
        public async Task<IActionResult> AllCategories()
        {
            var model = await categoryService.GetAllCategoriesAsync();
            return View(model);
        }
    }
}
