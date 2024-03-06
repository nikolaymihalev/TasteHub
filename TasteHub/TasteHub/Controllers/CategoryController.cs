using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;

namespace TasteHub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IRecipeService recipeService;
        

        public CategoryController(
            ICategoryService _categoryService,
            IRecipeService _recipeService)
        {
            categoryService = _categoryService;   
            recipeService = _recipeService;   
        }

        [HttpGet]
        public async Task<IActionResult> AllCategories()
        {
            var categories = await categoryService.GetAllCategoriesAsync();

            var model = categories.Select(x => new CategoryForAllModel(
                x.Id,
                x.Name,
                IsCategoryInUse(x.Name).Result));

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

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id) 
        {
            var category = await categoryService.GetByIdAsync(id);
            if (category == null) 
            {
                return BadRequest();
            }

            bool isInUse = await IsCategoryInUse(category.Name);

            if (isInUse) 
            {
                return BadRequest();
            }

            await categoryService.DeleteAsync(id);

            return RedirectToAction(nameof(AllCategories));
        }

        private async Task<bool> IsCategoryInUse(string name) 
        {
            var allRecipes = await recipeService.GetAllRecipesAsync();
            var recipes = allRecipes.Where(x => x.CategoryName == name);

            if (recipes.Any())
                return true;

            return false;
        }
    }
}
