using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public CategoryService(
            ApplicationDbContext _context,
            ILogger _logger)
        {
            context = _context;
            logger = _logger;
        }

        public async Task AddAsync(CategoryFormViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name
            };

            try
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CategoryService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(CategoryFormViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync()
        {
            return await context.Categories
                .AsNoTracking()
                .Select(x=> new CategoryInfoViewModel(
                    x.Id,
                    x.Name))
                .ToListAsync();
        }
    }
}
