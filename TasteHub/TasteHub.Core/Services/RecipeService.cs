﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public RecipeService(
            ApplicationDbContext _context, 
            ILogger _logger)
        {
            context = _context;
            logger = _logger;
        }

        public async Task AddAsync(RecipeFormViewModel model)
        {
            var recipe = new Recipe()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description == null ? string.Empty : model.Description,
                Ingredients = model.Ingredients,
                Instructions = model.Instructions,
                CreationDate = model.CreationDate,
                Image = model.Image,
                CategoryId = model.CategoryId,
                CreatorId = model.CreatorId
            };

            try
            {
                await context.Recipes.AddAsync(recipe);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "RecipeService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }

        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(RecipeFormViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RecipeInfoViewModel>> GetAllRecipesAsync()
        {
            return await context.Recipes
                .Select(x => new RecipeInfoViewModel(
                    x.Id,
                    x.Title,
                    x.Description == null ? string.Empty : x.Description,
                    x.Ingredients,
                    x.Instructions,
                    x.CreationDate.ToString(),
                    x.Image,
                    x.Creator.UserName,
                    x.Category.Name))
                .AsNoTracking()
                .ToListAsync();                
        }
    }
}
