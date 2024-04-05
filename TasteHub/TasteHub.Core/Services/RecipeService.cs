using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Recipe;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    /// <summary>
    /// Service for Recipe
    /// </summary>
    public class RecipeService : IRecipeService
    {
        private readonly IRepository repository;
        private readonly ILogger<RecipeService> logger;

        public RecipeService(
            IRepository _repository,
            ILogger<RecipeService> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        /// <summary>
        /// Add Recipe
        /// </summary>
        /// <param name="model">Recipe model</param>
        /// <exception cref="ApplicationException">Operation is failed</exception>
        public async Task AddAsync(RecipeFormViewModel model)
        {
            var recipe = new Recipe()
            {
                Title = model.Title,
                Description = model.Description,
                Ingredients = model.Ingredients,
                Instructions = model.Instructions,
                CreationDate = model.CreationDate,
                Image = model.Image,
                CategoryId = model.CategoryId,
                CreatorId = model.CreatorId
            };

            try
            {
                await repository.AddAsync<Recipe>(recipe);
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "RecipeService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        /// <summary>
        /// Delete Recipe
        /// </summary>
        /// <param name="id">Recipe identifier</param>
        /// <exception cref="ApplicationException">Model is invalid</exception>
        public async Task DeleteAsync(int id)
        {
            var recipe = await repository.GetByIdAsync<Recipe>(id);

            if (recipe == null)
            {
                logger.LogError("RecipeService.DeleteAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage,"recipe"));
            }

            await repository.DeleteAsync<Recipe>(recipe.Id);

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Edit Recipe
        /// </summary>
        /// <param name="model">Recipe model</param>
        /// <exception cref="ApplicationException">Model is invalid</exception>
        public async Task EditAsync(RecipeFormViewModel model)
        {
            var recipe = await repository.GetByIdAsync<Recipe>(model.Id);

            if(recipe == null) 
            {
                logger.LogError("RecipeService.EditAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "recipe"));
            }

            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Ingredients = model.Ingredients;
            recipe.Instructions = model.Instructions;
            recipe.CreationDate = model.CreationDate;
            recipe.Image = model.Image;
            recipe.CreatorId = model.CreatorId;
            recipe.CategoryId = model.CategoryId;

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Get all existing Recipes
        /// </summary>
        /// <returns>Collection of Recipe models</returns>
        public async Task<IEnumerable<RecipeInfoViewModel>> GetAllRecipesAsync()
        {
            return await repository.AllReadonly<Recipe>()
                .Select(x => new RecipeInfoViewModel(
                    x.Id,
                    x.Title,
                    x.Description == null ? string.Empty : x.Description,
                    x.Ingredients,
                    x.Instructions,
                    x.CreationDate,
                    Convert.ToBase64String(x.Image),
                    x.Creator.UserName,
                    x.Category.Name))
                .ToListAsync();
        }

        /// <summary>
        /// Get Recipe by identifier
        /// </summary>
        /// <param name="id">Recipe identifier</param>
        /// <returns>Recipe model or null</returns>
        /// <exception cref="ApplicationException">The entity doesn't exist</exception>
        public async Task<RecipeInfoViewModel?> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync<Recipe>(id);           

            if (entity == null) 
            {
                logger.LogError("RecipeService.GetByIdAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.DoesntExistErrorMessage, "recipe"));
            }
            var category = await repository.GetByIdAsync<Category>(entity.CategoryId);

            var model = new RecipeInfoViewModel(
                id,
                entity.Title,
                entity.Description,
                entity.Ingredients,
                entity.Instructions,
                entity.CreationDate,
                Convert.ToBase64String(entity.Image),                
                entity.CreatorId,
                category.Name);            

            return model;
        }

        /// <summary>
        /// Get the number of all recipes of user
        /// </summary>
        /// <param name="username">User username</param>
        /// <returns>Value for number of type int</returns>
        public async Task<int> GetRecipesCountByUserIdAsync(string userId)
        {
            return await repository.AllReadonly<Recipe>()
                .Where(x => x.CreatorId == userId)
                .CountAsync();
        }

        /// <summary>
        /// Get all recipes by searching them by title
        /// </summary>
        /// <param name="title">Recipe title</param>
        /// <returns>Collection of Recipe models</returns>
        public async Task<IEnumerable<RecipeInfoViewModel>> GetRecipesSearchedByTitleAsync(string title)
        {
            return await repository.AllReadonly<Recipe>()
                .Where(x=>x.Title.Contains(title))
                .Select(x => new RecipeInfoViewModel(
                    x.Id,
                    x.Title,
                    x.Description == null ? string.Empty : x.Description,
                    x.Ingredients,
                    x.Instructions,
                    x.CreationDate,
                    Convert.ToBase64String(x.Image),
                    x.Creator.UserName,
                    x.Category.Name))
                .ToListAsync();
        }

        /// <summary>
        /// Get recipes for page
        /// </summary>
        /// <param name="category">Category filter</param>
        /// <param name="sorting">Date filter</param>
        /// <param name="currentPage">Current page</param>
        /// <returns>Collection of recipe model</returns>
        public async Task<IEnumerable<RecipeInfoViewModel>> GetRecipesForPage(string? category=null, string? sorting=null,int currentPage = 1)
        {
            int formula = currentPage * ValidationConstants.MaxRecipesPerPage - 1;

            if (currentPage == 1) 
            {
                formula = 0;
            }

            var model = await GetAllRecipesAsync();

            if (category != null)
            {
                if (category.ToLower() != "all")
                {
                    model = model
                        .Where(x => x.CategoryName.ToLower() == category.ToLower())                        
                        .ToList();
                    foreach (var item in model) 
                    {
                        item.CategoryName = category;
                    }
                }
            }

            if (sorting != null)
            {
                if (sorting.ToLower() == "newest")
                {
                    model = model.OrderByDescending(x => x.CreationDate).ToList();
                    foreach (var item in model)
                    {
                        item.Sorting = sorting;
                    }
                }
                else if (sorting.ToLower() == "oldest")
                {
                    model = model.OrderBy(x => x.CreationDate).ToList();
                    foreach (var item in model)
                    {
                        item.Sorting = sorting;
                    }
                }
            }

            model = model                
                .Skip(formula)
                .Take(ValidationConstants.MaxRecipesPerPage);

            foreach (var item in model)
            {
                item.CurrentPage = currentPage;
            }

            return model;
        }
    }
}