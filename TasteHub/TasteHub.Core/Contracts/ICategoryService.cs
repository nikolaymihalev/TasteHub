using TasteHub.Core.Models.Category;

namespace TasteHub.Core.Contracts
{
    /// <summary>
    /// Interface for Category service
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="model">Category model</param>
        Task AddAsync(CategoryFormViewModel model);

        /// <summary>
        /// Edit Category
        /// </summary>
        /// <param name="model">Category model</param>
        Task EditAsync(CategoryFormViewModel model);

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id">Category identifier</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get Category by identifier
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>Category model or null</returns>
        Task<CategoryInfoViewModel?> GetByIdAsync(int id);

        /// <summary>
        /// Get Category by name
        /// </summary>
        /// <param name="name">Category name</param>
        /// <returns>Category model or null</returns>
        Task<CategoryInfoViewModel?> GetByNameAsync(string name);

        /// <summary>
        /// Get all existing categories
        /// </summary>
        /// <returns>Collection of Category models</returns>
        Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync();
    }
}
