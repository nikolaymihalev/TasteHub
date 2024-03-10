﻿using TasteHub.Core.Models;

namespace TasteHub.Core.Contracts
{
    public interface IRatingService
    {
        Task AddAsync();
        Task DeleteAsync(int id);
        Task<double> GetAverageRatingAboutRecipeAsync(int recipeId);
        Task<IEnumerable<RecipeInfoViewModel>> GetAllRatingsAboutRecipeAsync(int recipeId);
    }
}
