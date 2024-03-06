using TasteHub.Core.Models;

namespace TasteHub.Core.Contracts
{
    public interface IFavoriteRecipeService
    {
        Task<IEnumerable<FavoriteRecipeInfoModel>> GetAllFavoriteRecipesAsync();
        Task AddAsync();
        Task RemoveAsync(int id);
    }
}
