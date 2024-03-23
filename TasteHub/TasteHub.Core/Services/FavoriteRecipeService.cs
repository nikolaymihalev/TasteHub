using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Core.Models.Recipe;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    /// <summary>
    /// Service for Favorite Recipe
    /// </summary>
    public class FavoriteRecipeService : IFavoriteRecipeService
    {
        private readonly IRepository repository;
        private readonly ILogger<FavoriteRecipeService> logger;

        public FavoriteRecipeService(
            IRepository _repository,
            ILogger<FavoriteRecipeService> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        /// <summary>
        /// Add Favorite recipe
        /// </summary>
        /// <param name="model">Favorite Recipe model</param>
        /// <exception cref="ApplicationException">Operation is failed</exception>
        public async Task AddAsync(FavoriteRecipeInfoModel model)
        {
            var entity = new FavoriteRecipe() 
            {
                UserId = model.UserId,
                RecipeId = model.RecipeId
            };

            try
            {
                await repository.AddAsync<FavoriteRecipe>(entity);
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "FavoriteRecipeService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        /// <summary>
        /// Get all existing favorite recipes
        /// </summary>
        /// <returns>Collection of Favortie Recipe models</returns>
        public async Task<IEnumerable<FavoriteRecipeInfoModel>> GetAllFavoriteRecipesAsync()
        {
            return await repository.AllReadonly<FavoriteRecipe>()
                .Select(x=> new FavoriteRecipeInfoModel(
                    x.User.Id,
                    x.RecipeId,
                    new RecipeInfoViewModel(x.Recipe.Id,
                        x.Recipe.Title,
                        x.Recipe.Description == null ? string.Empty : x.Recipe.Description,
                        x.Recipe.Ingredients,
                        x.Recipe.Instructions,
                        x.Recipe.CreationDate,
                        Convert.ToBase64String(x.Recipe.Image),
                        x.Recipe.Creator.UserName,
                        x.Recipe.Category.Name)))
                .ToListAsync();
        }

        /// <summary>
        /// Remove Favorite recipe
        /// </summary>
        /// <param name="id">Favorite Recipe identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ApplicationException">Operation is failed</exception>
        public async Task RemoveAsync(int id, string userId)
        {
            try
            {
                repository.Delete(new FavoriteRecipe(){ RecipeId = id, UserId = userId });
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "FavoriteRecipeService.RemoveAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        /// <summary>
        /// Delete collection of Favorite recipes
        /// </summary>
        /// <param name="models">Collecton of Favorite Recipe models</param>
        public void DeleteRange(IEnumerable<FavoriteRecipeInfoModel> models) 
        {
            var entites = models.Select(x => new FavoriteRecipe()
            {
                RecipeId = x.RecipeId,
                UserId = x.UserId
            });
            repository.DeleteRange(entites);
        }

        /// <summary>
        /// Get all favorite recipes for user
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Collection of Favortie Recipe models</returns>
        public async Task<IEnumerable<FavoriteRecipeInfoModel>> GetAllFavoriteRecipesForUserAsync(string userId)
        {
            return await repository.AllReadonly<FavoriteRecipe>()
                .Where(x=>x.UserId==userId)
                .Select(x => new FavoriteRecipeInfoModel(
                    x.User.Id,
                    x.RecipeId,
                    new RecipeInfoViewModel(x.Recipe.Id,
                        x.Recipe.Title,
                        x.Recipe.Description == null ? string.Empty : x.Recipe.Description,
                        x.Recipe.Ingredients,
                        x.Recipe.Instructions,
                        x.Recipe.CreationDate,
                        Convert.ToBase64String(x.Recipe.Image),
                        x.Recipe.Creator.UserName,
                        x.Recipe.Category.Name)))
                .ToListAsync();
        }
    }
}
