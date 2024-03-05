using TasteHub.Core.Models;

namespace TasteHub.Core.Contracts
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryFormViewModel model);
        Task EditAsync(CategoryFormViewModel model);
        Task DeleteAsync(int id);
        Task<CategoryInfoViewModel?> GetByIdAsync(int id);
        Task<CategoryInfoViewModel?> GetByNameAsync(string name);
        Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync();
    }
}
