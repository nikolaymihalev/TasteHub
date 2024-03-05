using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
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

        public async Task DeleteAsync(int id)
        {
            var category = await repository.GetByIdAsync<Category>(id);

            if (category == null)
            {
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "category"));
            }

            await repository.DeleteAsync<Category>(category);

            await repository.SaveChangesAsync();
        }

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

        public async Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync()
        {
            return repository.AllReadonly<Category>()
                .Select(x => new CategoryInfoViewModel(
                    x.Id,
                    x.Name));
        }

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
