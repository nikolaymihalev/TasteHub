using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;

namespace TasteHub.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly IRecipeService recipeService;
        private readonly ICategoryService categoryService;

        public RecipeController(
            IRecipeService _recipeService, 
            ICategoryService _categoryService)
        {
            recipeService = _recipeService;
            categoryService = _categoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AllRecipes()
        {
            var model = await recipeService.GetAllRecipesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddRecipe() 
        {
            var model = new RecipeFormViewModel()
            {
                Categories = await categoryService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            await recipeService.AddAsync(model);

            return RedirectToAction(nameof(AllRecipes));
        }
    }
}
