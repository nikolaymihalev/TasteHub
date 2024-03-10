namespace TasteHub.Core.Contracts
{
    public interface IRatingService
    {
        Task AddAsync();
        Task DeleteAsync();
        Task GetAverageRatingAboutRecipeAsync(int recipeId);
        Task GetAllRatingsAboutRecipeAsync(int recipeId);
    }
}
