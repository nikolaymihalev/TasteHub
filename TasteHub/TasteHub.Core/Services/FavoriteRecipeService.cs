﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
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

        public async Task AddAsync(FavoriteRecipeFormModel model)
        {
            var entity = new FavoriteRecipe() 
            {
                UserId = model.CreatorId,
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

        public async Task<IEnumerable<FavoriteRecipeInfoModel>> GetAllFavoriteRecipesAsync()
        {
            return await repository.AllReadonly<FavoriteRecipe>()
                .Select(x=> new FavoriteRecipeInfoModel(
                    x.User.Id,
                    x.User.UserName,
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

        public async Task RemoveAsync(int id, string userId)
        {
            try
            {
                repository.Delete(new { RecipeId = id, UserId = userId });
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "FavoriteRecipeService.RemoveAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }
    }
}
