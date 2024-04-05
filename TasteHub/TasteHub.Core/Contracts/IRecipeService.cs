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
        /// <param name="userId">User identifier</param>
        /// <returns>Value for number of type int</returns>
        Task<int> GetRecipesCountByUserIdAsync(string userId);

        /// <summary>
        /// Get recipes for page
        /// </summary>
        /// <param name="category">Category filter</param>
        /// <param name="sorting">Date filter</param>
        /// <param name="currentPage">Current page</param>
        /// <returns>Collection of recipe model</returns>
        Task<RecipeQueryModel> GetRecipesForPage(string? category = null, string? sorting = null, int currentPage=1);
    }
}