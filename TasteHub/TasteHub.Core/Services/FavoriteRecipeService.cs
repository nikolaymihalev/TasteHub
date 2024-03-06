using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    public class FavoriteRecipeService : IFavoriteRecipeService
    {
        private readonly IRepository repository;
        private readonly ILogger<FavoriteRecipeService> logger;

        public FavoriteRecipeService(
            IRepository _repository,
            ILogger<FavoriteRecipeService> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        public Task AddAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FavoriteRecipeInfoModel>> GetAllFavoriteRecipesAsync()
        {
            return await repository.AllReadonly<FavoriteRecipe>()
                .Select(x=> new FavoriteRecipeInfoModel(
                    x.User.Id,
                    x.User.UserName,
                    x.RecipeId))
                .ToListAsync();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
