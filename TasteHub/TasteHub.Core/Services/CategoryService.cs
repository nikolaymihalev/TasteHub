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
        private readonly ILogger logger;

        public CategoryService(
            IRepository _repository,
            ILogger _logger)
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
    }
}
