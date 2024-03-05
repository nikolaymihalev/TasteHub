using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [HttpGet]
        [AllowAnonymous]
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
            if (ModelState.IsValid==false)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.ImageFile.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    model.Image = imageBytes;
                    model.CreatorId = User.Id();
                    model.CreationDate = DateTime.Now;

                    await recipeService.AddAsync(model);    
                }
                return RedirectToAction(nameof(AllRecipes));
            }

            model.Categories = await categoryService.GetAllCategoriesAsync();
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id) 
        {
            var model = await recipeService.GetByIdAsync(id);

            if(model == null) 
            {
                return BadRequest();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRecipe(int id) 
        {
            var recipe = await recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                return BadRequest();
            }

            if (recipe.CreatorId != User.Id()) 
            {
                return Unauthorized();
            }

            var category = await categoryService.GetByNameAsync(recipe.CategoryName);

            var model = new RecipeFormViewModel()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                CreationDate = recipe.CreationDate,
                CreatorId = recipe.CreatorId,
                CategoryId = category.Id
            };

            return View(model);
        }
    }
}
