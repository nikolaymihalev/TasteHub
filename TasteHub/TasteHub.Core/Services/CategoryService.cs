using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Data;

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

        public Task AddAsync(CategoryFormViewModel model)
        {
            throw new NotImplementedException();
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
