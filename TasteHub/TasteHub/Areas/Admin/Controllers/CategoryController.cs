using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Category;

namespace TasteHub.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
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

            TempData["Successful"] = $"You have successfully added a category with name {model.Name}!";

            return RedirectToAction(nameof(AllCategories));
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            try
            {
                var category = await categoryService.GetByIdAsync(id);

                var model = new CategoryFormViewModel()
                {
                    Id = id,
                    Name = category.Name,
                };

                return View(model);
            }
            catch (Exception)
            {
                TempData["Danger"] = $"This category doesn't exist!";

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryFormViewModel model, int id)
        {
            try
            {
                var category = await categoryService.GetByIdAsync(id);
            }
            catch (Exception)
            {
                TempData["Danger"] = $"This category doesn't exist!";

                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.EditAsync(model);

            TempData["Successful"] = $"You have successfully edited a category with name {model.Name}!";

            return RedirectToAction(nameof(AllCategories));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await categoryService.GetByIdAsync(id);
                bool isInUse = await IsCategoryInUse(category.Name);

                if (isInUse)
                {
                    TempData["Danger"] = $"This category is already in use! You can't delete it!";

                    return BadRequest();
                }

                await categoryService.DeleteAsync(id);
            }
            catch (Exception)
            {
                TempData["Danger"] = $"Delete operation is failed!";

                return BadRequest();
            }

            TempData["Successful"] = $"You have successfully deleted a category!";

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
