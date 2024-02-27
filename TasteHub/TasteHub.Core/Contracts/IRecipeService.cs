using TasteHub.Core.Models;

namespace TasteHub.Core.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeInfoViewModel>> GetAllRecipesAsync();
        Task AddAsync(RecipeFormViewModel model);
        Task EditAsync(RecipeFormViewModel model);
        Task DeleteAsync(int id);
    }
}