using TasteHub.Core.Models.Recipe;

namespace TasteHub.Core.Contracts
{
    /// <summary>
    /// Interface for Recipe service
    /// </summary>
    public interface IRecipeService
    {
        /// <summary>
        /// Get all existing Recipes
        /// </summary>
        /// <returns>Collection of Recipe models</returns>
        Task<IEnumerable<RecipeInfoViewModel>> GetAllRecipesAsync();

        /// <summary>
        /// Get all recipes by searching them by title
        /// </summary>
        /// <param name="title">Recipe title</param>
        /// <returns>Collection of Recipe models</returns>
        Task<IEnumerable<RecipeInfoViewModel>> GetRecipesSearchedByTitleAsync(string title);

        /// <summary>
        /// Get all recipes filtered by category name
        /// </summary>
        /// <param name="category">Category name</param>
        /// <returns>Collection of Recipe models</returns>
        Task<IEnumerable<RecipeInfoViewModel>> GetRecipesFilteredByCategory(string category);

        /// <summary>
        /// Get all recipes filtered by published date
        /// </summary>
        /// <param name="sorting">Filter for ordering (newest/oldest)</param>
        /// <returns>Collection of Recipe models</returns>
        Task<IEnumerable<RecipeInfoViewModel>> GetRecipesFilteredByDate(string sorting);       

        /// <summary>
        /// Add Recipe
        /// </summary>
        /// <param name="model">Recipe model</param>
        Task AddAsync(RecipeFormViewModel model);

        /// <summary>
        /// Edit Recipe
        /// </summary>
        /// <param name="model">Recipe model</param>
        Task EditAsync(RecipeFormViewModel model);

        /// <summary>
        /// Get Recipe by identifier
        /// </summary>
        /// <param name="id">Recipe identifier</param>
        /// <returns>Recipe model or null</returns>
        Task<RecipeInfoViewModel?> GetByIdAsync(int id);

        /// <summary>
        /// Delete Recipe
        /// </summary>
        /// <param name="id">Recipe identifier</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get the number of all recipes of user
        /// </summary>
        /// <param name="username">User username</param>
        /// <returns>Value for number of type int</returns>
        Task<int> GetRecipesCountByUsernameAsync(string username);
    }
}