﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IFavoriteRecipeService favoriteRecipeService;

        public RecipeController(
            IRecipeService _recipeService, 
            ICategoryService _categoryService,
            IFavoriteRecipeService _favoriteRecipeService)
        {
            recipeService = _recipeService;
            categoryService = _categoryService;
            favoriteRecipeService = _favoriteRecipeService;
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

            var favorites = await favoriteRecipeService.GetAllFavoriteRecipesAsync();

            bool isInFavorite = favorites.Any(x => x.UserId == User.Id()) && favorites.Any(x => x.RecipeId == id);

            model.IsInUserFavoriteCollection = isInFavorite;
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
                CategoryId = category.Id,
                Categories = await categoryService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRecipe(RecipeFormViewModel model,int id) 
        {
            var recipe = await recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return BadRequest();
            }

            if (recipe.CreatorId != User.Id())
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return Unauthorized();
            }

            if (!ModelState.IsValid) 
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return View(model);
            }
            model.CreatorId = User.Id();

            using (var memoryStream = new MemoryStream())
            {
                model.ImageFile.CopyTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                model.Image = imageBytes;
            }

            await recipeService.EditAsync(model);

            return RedirectToAction(nameof(AllRecipes));            
        }

        [HttpGet]
        public async Task<IActionResult> Mine() 
        {
            var recipes = await recipeService.GetAllRecipesAsync();

            var model = recipes.Where(x => x.CreatorId == User?.Identity?.Name);

            return View(model);
        }
    }
}