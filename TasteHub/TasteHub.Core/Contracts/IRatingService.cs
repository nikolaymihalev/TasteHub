﻿using TasteHub.Core.Models.Rating;

namespace TasteHub.Core.Contracts
{
    public interface IRatingService
    {
        Task AddAsync(RatingFormModel model);
        Task DeleteAsync(int recipeId, string userId);
        Task EditAsync(RatingFormModel model);
        Task<double> GetAverageRatingAboutRecipeAsync(int recipeId);
        Task<IEnumerable<RatingInfoModel>> GetAllRatingsAboutRecipeAsync(int recipeId);
        void DeleteRange(IEnumerable<RatingInfoModel> models);
    }
}
