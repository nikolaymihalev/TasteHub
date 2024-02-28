using Microsoft.EntityFrameworkCore;
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
                await context.Recipes.AddAsync(recipe);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "RecipeService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                logger.LogError("RecipeService.DeleteAsync");
                throw new ApplicationException(ErrorMessageConstants.InvalidRecipeErrorMessage);
            }

            context.Remove(recipe);

            await context.SaveChangesAsync();
        }

        public async Task EditAsync(RecipeFormViewModel model)
        {
            var recipe = await context.Recipes.FindAsync(model.Id);

            if(recipe == null) 
            {
                logger.LogError("RecipeService.EditAsync");
                throw new ApplicationException(ErrorMessageConstants.InvalidRecipeErrorMessage);
            }

            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Ingredients = model.Ingredients;
            recipe.Instructions = model.Instructions;
            recipe.CreationDate = model.CreationDate;
            recipe.Image = model.Image;
            recipe.CreatorId = model.CreatorId;
            recipe.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();
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

        public async Task<RecipeInfoViewModel?> GetByIdAsync(int id)
        {
            return await context.Recipes
                .AsNoTracking()
                .Where(x=>x.Id==id)
                .Select(x=> new RecipeInfoViewModel(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Ingredients,
                    x.Instructions,
                    x.CreationDate.ToString(),
                    x.Image,
                    x.Creator.UserName,
                    x.Category.Name))
                .FirstOrDefaultAsync();
        }
    }
}
