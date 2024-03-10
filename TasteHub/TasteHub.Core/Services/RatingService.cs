using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Common;

namespace TasteHub.Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepository repository;
        private readonly ILogger<RatingService> logger;

        public RatingService(
            IRepository _repository, 
            ILogger<RatingService> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        public Task AddAsync(RatingFormModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RecipeInfoViewModel>> GetAllRatingsAboutRecipeAsync(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetAverageRatingAboutRecipeAsync(int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
