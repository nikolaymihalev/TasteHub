using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Category;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    /// <summary>
    /// Service for Category
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;
        private readonly ILogger<CategoryService> logger;

        public CategoryService(
            IRepository _repository,
            ILogger<CategoryService> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="model">Category model</param>
        /// <exception cref="ApplicationException">Operation is failed</exception>
        public async Task AddAsync(CategoryFormViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name
            };

            try
            {
                await repository.AddAsync<Category>(category);
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CategoryService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <exception cref="ApplicationException">Model is invalid</exception>
        public async Task DeleteAsync(int id)
        {
            var category = await repository.GetByIdAsync<Category>(id);

            if (category == null)
            {
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "category"));
            }

            await repository.DeleteAsync<Category>(category.Id);

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Edit Category
        /// </summary>
        /// <param name="model">Category model</param>
        /// <exception cref="ApplicationException">Model is invalid</exception>
        public async Task EditAsync(CategoryFormViewModel model)
        {
            var category = await repository.GetByIdAsync<Category>(model.Id);

            if (category == null)
            {
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "category"));
            }

            category.Name = model.Name;

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Get all existing categories
        /// </summary>
        /// <returns>Collection of Category models</returns>
        public async Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync()
        {
            return await repository.AllReadonly<Category>()
                .Select(x => new CategoryInfoViewModel(
                    x.Id,
                    x.Name))
                .ToListAsync();
        }

        /// <summary>
        /// Get Category by identifier
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>Category model or null</returns>
        /// <exception cref="ApplicationException">The entity doesn't exist</exception>
        public async Task<CategoryInfoViewModel?> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync<Category>(id);

            if (entity == null)
            {
                logger.LogError("CategoryService.GetByIdAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.DoesntExistErrorMessage, "category"));
            }
            var model = new CategoryInfoViewModel(
                id,
                entity.Name);

            return model;
        }

        /// <summary>
        /// Get Category by name
        /// </summary>
        /// <param name="name">Category name</param>
        /// <returns>Category model or null</returns>
        /// <exception cref="ApplicationException">The entity doesn't exist</exception>
        public async Task<CategoryInfoViewModel?> GetByNameAsync(string name)
        {
            var entity = await repository.AllReadonly<Category>().Where(x=>x.Name==name).FirstOrDefaultAsync();

            if (entity == null)
            {
                logger.LogError("CategoryService.GetByNameAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.DoesntExistErrorMessage, "category"));
            }
            var model = new CategoryInfoViewModel(
                entity.Id,
                entity.Name);

            return model;
        }
    }
}
