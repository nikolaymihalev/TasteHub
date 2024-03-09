using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
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

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CommentInfoModel>> GetAllCommentsAboutRecipeAsync(int recipeId)
        {
            return await repository.AllReadonly<Comment>()
                .Select(x => new CommentInfoModel(
                    x.Id,
                    x.Content,
                    x.CreationDate,
                    x.UserId,
                    x.User.UserName,
                    x.RecipeId))
                .Where(x=>x.RecipeId==recipeId)
                .ToListAsync();
        }

        public async Task<CommentInfoModel> GetLastCommentAboutRecipeAsync(int recipeId)
        {
            var comments = await GetAllCommentsAboutRecipeAsync(recipeId);

            return comments.OrderByDescending(x=>x.CreationDate).FirstOrDefault();
        }
    }
}
