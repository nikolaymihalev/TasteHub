using TasteHub.Core.Models;

namespace TasteHub.Core.Contracts
{
    /// <summary>
    /// Interface for Favorite Recipe service
    /// </summary>
    public interface IFavoriteRecipeService
    {
        /// <summary>
        /// Get all existing favorite recipes
        /// </summary>
        /// <returns>Collection of Favortie Recipe models</returns>
        Task<IEnumerable<FavoriteRecipeInfoModel>> GetAllFavoriteRecipesAsync();

        /// <summary>
        /// Get all favorite recipes for user
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Collection of Favortie Recipe models</returns>
        Task<IEnumerable<FavoriteRecipeInfoModel>> GetAllFavoriteRecipesForUserAsync(string userId);

        /// <summary>
        /// Add Favorite recipe
        /// </summary>
        /// <param name="model">Favorite Recipe model</param>
        Task AddAsync(FavoriteRecipeInfoModel model);

        /// <summary>
        /// Remove Favorite recipe
        /// </summary>
        /// <param name="id">Favorite Recipe identifier</param>
        /// <param name="userId">User identifier</param>
        Task RemoveAsync(int id,string userId);

        /// <summary>
        /// Delete Favorite recipe
        /// </summary>
        Task DeleteAsync(int recipeId, string userId);
    }
}
