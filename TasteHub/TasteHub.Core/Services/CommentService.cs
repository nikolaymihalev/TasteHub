using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Comment;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    /// <summary>
    /// Service for Comment
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly IRepository repository;
        private readonly ILogger<CommentService> logger;

        public CommentService(
            IRepository _repository,
            ILogger<CommentService> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        /// <summary>
        /// Add Comment
        /// </summary>
        /// <param name="model">Comment model</param>
        /// <exception cref="ApplicationException">Operation is failed</exception>
        public async Task AddSync(CommentFormModel model)
        {
            var entity = new Comment()
            {
                Content = model.Content,
                CreationDate = DateTime.Now,
                UserId = model.UserId,
                RecipeId = model.RecipeId,
            };

            try
            {
                await repository.AddAsync<Comment>(entity);
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CommentService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="id">Comment identifier</param>
        /// <exception cref="ApplicationException">Model is invalid</exception>
        public async Task DeleteAsync(int id)
        {
            var comment = await repository.GetByIdAsync<Comment>(id);

            if (comment == null)
            {
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "comment"));
            }

            await repository.DeleteAsync<Comment>(comment.Id);

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Get all existing comments about recipe
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <returns>Collection of Comment models</returns>
        public async Task<IEnumerable<CommentInfoModel>> GetAllCommentsAboutRecipeAsync(int recipeId)
        {
            return await repository.AllReadonly<Comment>()
                .Where(x=>x.RecipeId==recipeId)
                .Select(x => new CommentInfoModel(
                    x.Id,
                    x.Content,
                    x.CreationDate,
                    x.UserId,
                    x.User.UserName,
                    x.RecipeId,
                    x.Recipe.Title))
                .ToListAsync();
        }

        /// <summary>
        /// Get last comment about recipe
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <returns>Comment model or null</returns>
        public async Task<CommentInfoModel?> GetLastCommentAboutRecipeAsync(int recipeId)
        {
            var comments = await GetAllCommentsAboutRecipeAsync(recipeId);

            return comments.OrderByDescending(x=>x.CreationDate).FirstOrDefault();
        }

        /// <summary>
        /// Get Comment by identifier
        /// </summary>
        /// <param name="id">Comment identifier</param>
        /// <returns>Comment model</returns>
        /// <exception cref="ApplicationException">The entity doesn't exist</exception>
        public async Task<CommentInfoModel> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync<Comment>(id);

            if (entity == null)
            {
                logger.LogError("CommentService.GetByIdAsync");
                throw new ApplicationException(string.Format(ErrorMessageConstants.DoesntExistErrorMessage, "comment"));
            }

            var comment = new CommentInfoModel(
                entity.Id,
                entity.Content,
                entity.CreationDate,
                entity.UserId,
                "",
                entity.RecipeId,
                "");
            
            return comment;
        }

        /// <summary>
        /// Delete range of comments
        /// </summary>
        /// <param name="models">Collection of Comment models</param>
        public void DeleteRange(IEnumerable<CommentInfoModel> models)
        {
            var entites = models.Select(x => new Comment()
            {
                Id = x.Id, 
                Content = x.Content,
                CreationDate = x.CreationDate,
                RecipeId = x.RecipeId,
                UserId = x.UserId
            });
            repository.DeleteRange(entites);
        }
    }
}
