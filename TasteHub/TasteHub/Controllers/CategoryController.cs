using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;

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

        [HttpGet]
        public IActionResult AddCategory() 
        {
            var model = new CategoryFormViewModel();
            return View(model);
        }
    }
}
