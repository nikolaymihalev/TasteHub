using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Core.Services;

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

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryFormViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            await categoryService.AddAsync(model);

            return RedirectToAction(nameof(AllCategories));
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id) 
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return BadRequest();
            }

            var model = new CategoryFormViewModel()
            {
                Id = id,
                Name = category.Name,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryFormViewModel model, int id) 
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.EditAsync(model);

            return RedirectToAction(nameof(AllCategories));
        }
    }
}
