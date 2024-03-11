using TasteHub.Core.Models;

namespace TasteHub.Core.Contracts
{
    public interface IRatingService
    {
        Task AddAsync(RatingFormModel model);
        Task DeleteAsync(int recipeId, string userId);
        Task<double> GetAverageRatingAboutRecipeAsync(int recipeId);
        Task<IEnumerable<RatingInfoModel>> GetAllRatingsAboutRecipeAsync(int recipeId);
    }
}
