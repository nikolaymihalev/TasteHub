using TasteHub.Core.Models;

namespace TasteHub.Core.Contracts
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryFormViewModel model);
        Task EditAsync(CategoryFormViewModel model);
        Task DeleteAsync(int id);
        Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync();
    }
}
