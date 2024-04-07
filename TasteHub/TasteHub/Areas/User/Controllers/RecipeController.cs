using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Recipe;
using TasteHub.Infrastructure.Constants;

namespace TasteHub.Areas.User.Controllers
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
        public async Task<IActionResult> AllRecipes(string category, string sorting,int currentPage = 1)
        {
            var model = await recipeService.GetRecipesForPageAsync(category,sorting,currentPage);

            var categories = await categoryService.GetAllCategoriesAsync();
            if (categories.Any())
            {
                ViewBag.Categories = categories;
            }

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
            if (ModelState.IsValid == false)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            if (model.ImageFile == null)
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

            if (model == null)
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
        public async Task<IActionResult> EditRecipe(RecipeFormViewModel model, int id)
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

            if (model.ImageFile == null)
            {
                model.Image = Convert.FromBase64String(recipe.Image);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            model.CreatorId = User.Id();
            model.CreationDate = recipe.CreationDate;

            if (model.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.ImageFile.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    model.Image = imageBytes;
                }
            }

            await recipeService.EditAsync(model);

            return RedirectToAction(nameof(AllRecipes));
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var recipes = await recipeService.GetAllRecipesAsync();

            var model = recipes.Where(x => x.CreatorId == User?.Identity?.Name);

            foreach (var recipe in model)
            {
                var lastRecipeComment = await commentService.GetLastCommentAboutRecipeAsync(recipe.Id);

                if (lastRecipeComment != null)
                {
                    recipe.LastComment = lastRecipeComment;
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                return BadRequest();
            }

            if (recipe.CreatorId != User.Id() && User.IsInRole("Admin")==false)
            {
                return Unauthorized();
            }

            var allFr = await favoriteRecipeService.GetAllFavoriteRecipesAsync();
            var frToDelete = allFr.Where(x => x.RecipeId == id).ToList();

            if (frToDelete.Any())
            {
                foreach (var favorite in frToDelete)
                {
                    await favoriteRecipeService.DeleteAsync(favorite.RecipeId, favorite.UserId);
                }
            }

            var allCom = await commentService.GetAllCommentsAboutRecipeAsync(id);
            if (allCom.Any())
            {
                foreach (var comment in allCom)
                {
                    await commentService.DeleteAsync(comment.Id);
                }
            }

            var allRat = await ratingService.GetAllRatingsAboutRecipeAsync(id);
            if (allRat.Any())
            {
                foreach (var rating in allRat)
                {
                    await ratingService.DeleteAsync(rating.Id);
                }
            }

            await recipeService.DeleteAsync(id);

            return RedirectToAction(nameof(AllRecipes));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest();
            }
            var model = await recipeService.GetRecipesSearchedByTitleAsync(title);

            var categories = await categoryService.GetAllCategoriesAsync();
            if (categories.Any())
            {
                ViewBag.Categories = categories;
            }
            return View(model);
        }
    }}