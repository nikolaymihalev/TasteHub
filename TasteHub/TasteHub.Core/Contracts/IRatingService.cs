using TasteHub.Core.Models.Rating;

namespace TasteHub.Core.Contracts
{
    /// <summary>
    /// Interface for Rating service
    /// </summary>
    public interface IRatingService
    {
        /// <summary>
        /// Add Rating 
        /// </summary>
        /// <param name="model">Rating model</param>
        Task AddAsync(RatingFormModel model);
        
        /// <summary>
        /// Delete Rating
        /// </summary>
        /// <param name="id">Rating identifier</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Edit Rating
        /// </summary>
        /// <param name="model">Rating model</param>
        Task EditAsync(RatingFormModel model);

        /// <summary>
        /// Get average rating about recipe
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <returns>Value of type double</returns>
        Task<double> GetAverageRatingAboutRecipeAsync(int recipeId);

        /// <summary>
        /// Get all ratings about recipe
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <returns>Collection of Rating models</returns>
        Task<IEnumerable<RatingInfoModel>> GetAllRatingsAboutRecipeAsync(int recipeId);
    }
}
