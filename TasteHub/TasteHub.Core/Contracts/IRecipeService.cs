using TasteHub.Core.Models.Recipe;

namespace TasteHub.Core.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeInfoViewModel>> GetAllRecipesAsync();
        Task<IEnumerable<RecipeInfoViewModel>> GetRecipesSearchedByTitleAsync(string title);
        Task<IEnumerable<RecipeInfoViewModel>> GetRecipesFilteredByCategory(string category);
        Task<IEnumerable<RecipeInfoViewModel>> GetRecipesFilteredByDate(string sorting);       
        Task AddAsync(RecipeFormViewModel model);
        Task EditAsync(RecipeFormViewModel model);
        Task<RecipeInfoViewModel?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}