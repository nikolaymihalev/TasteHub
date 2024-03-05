using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
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

        public async Task DeleteAsync(int id)
        {
            var recipe = await repository.GetByIdAsync<Recipe>(id);

            if (recipe == null)
            {
                logger.LogError("RecipeService.DeleteAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage,"recipe"));
            }

            await repository.DeleteAsync<Recipe>(recipe);

            await repository.SaveChangesAsync();
        }

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
    }
}