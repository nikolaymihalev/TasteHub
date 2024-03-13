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
        private readonly ICommentService commentService;
        private readonly IRatingService ratingService;

        public RecipeController(
            IRecipeService _recipeService, 
            ICategoryService _categoryService,
            IFavoriteRecipeService _favoriteRecipeService,
            ICommentService _commentService,
            IRatingService _ratingService)
        {
            recipeService = _recipeService;
            categoryService = _categoryService;
            favoriteRecipeService = _favoriteRecipeService;
            commentService = _commentService;
            ratingService = _ratingService;
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

            var comment = await commentService.GetLastCommentAboutRecipeAsync(id);

            if (comment != null) 
            {
                model.LastComment = comment;
            }

            double averageRating = await ratingService.GetAverageRatingAboutRecipeAsync(id);
            
            model.AverageRating = averageRating;

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRecipe(int id) 
        {
            var recipe = await recipeService.GetByIdAsync(id);

            if (recipe == null) 
            {
                return BadRequest();
            }

            if (!User.IsInRole("Admin")) 
            {
                return Unauthorized();
            }

            var allFr = await favoriteRecipeService.GetAllFavoriteRecipesAsync();
            var frToDelete = allFr.Where(x => x.RecipeId == id).ToList();

            if (frToDelete.Any())
            {
                favoriteRecipeService.DeleteRange(frToDelete);
            }

            var allCom = await commentService.GetAllCommentsAboutRecipeAsync(id);
            if (allCom.Any()) 
            {
                commentService.DeleteRange(allCom);
            }

            var allRat = await ratingService.GetAllRatingsAboutRecipeAsync(id);
            if (allRat.Any()) 
            {
                ratingService.DeleteRange(allRat);
            }

            await recipeService.DeleteAsync(id);

            return RedirectToAction(nameof(AllRecipes));
        }
    }
}