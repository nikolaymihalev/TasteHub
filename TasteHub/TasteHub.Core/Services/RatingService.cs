using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Rating;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    /// <summary>
    /// Service for Rating 
    /// </summary>
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

        /// <summary>
        /// Add Rating 
        /// </summary>
        /// <param name="model">Rating model</param>
        /// <exception cref="ApplicationException">Operation is failed</exception>
        public async Task AddAsync(RatingFormModel model)
        {
            var entity = new Rating()
            {
                UserId = model.UserId,
                RecipeId = model.RecipeId,
                Value = model.Value,
            };

            try
            {
                await repository.AddAsync<Rating>(entity);
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "RatingService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        /// <summary>
        /// Delete Rating
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <param name="userId">User identifier</param>
        /// <exception cref="ApplicationException">Model is invalid</exception>
        public async Task DeleteAsync(int recipeId, string userId)
        {
            var rating = await repository
                .AllReadonly<Rating>()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.RecipeId == recipeId);

            if (rating == null)
            {
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "rating"));
            }

            repository.Delete(rating);

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Edit Rating
        /// </summary>
        /// <param name="model">Rating model</param>
        /// <exception cref="ApplicationException">Model is invalid</exception>
        public async Task EditAsync(RatingFormModel model)
        {
            var allRatings = repository.AllReadonly<Rating>();

            if (!allRatings.Any())
            {
                logger.LogError("RatingService.EditAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "rating"));
            }

            var rating = allRatings.FirstOrDefault(x => x.UserId == model.UserId && x.RecipeId == model.RecipeId);

            if(rating == null)
            {
                logger.LogError("RatingService.EditAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "rating"));
            }

            await DeleteAsync(model.RecipeId, model.UserId);
            await AddAsync(model);

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Get all ratings about recipe
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <returns>Collection of Rating models</returns>
        public async Task<IEnumerable<RatingInfoModel>> GetAllRatingsAboutRecipeAsync(int recipeId)
        {
            return await repository.AllReadonly<Rating>()
                .Where(x => x.RecipeId == recipeId)
                .Select(x => new RatingInfoModel(
                    x.UserId,
                    x.User.UserName,
                    x.RecipeId,
                    x.Recipe.Title,
                    x.Value))
                .ToListAsync();
        }

        /// <summary>
        /// Get average rating about recipe
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <returns>Value of type double</returns>
        public async Task<double> GetAverageRatingAboutRecipeAsync(int recipeId)
        {
            if (repository.AllReadonly<Rating>().Any(x => x.RecipeId == recipeId)) 
            {
                return repository.AllReadonly<Rating>()
                    .Where(x=>x.RecipeId == recipeId)
                    .Average(x=>x.Value);
            }

            return 0;
        }
    }
}
