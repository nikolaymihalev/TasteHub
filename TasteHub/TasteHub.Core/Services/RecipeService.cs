using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Data;

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

        public Task AddAsync(RecipeFormViewModel model)
        {
            throw new NotImplementedException();
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
