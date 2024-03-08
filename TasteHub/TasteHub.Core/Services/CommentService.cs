using Microsoft.EntityFrameworkCore;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository repository;


        public CommentService(IRepository _repository)
        {
            repository = _repository;
        }

        public Task AddSync(CommentFormModel model)
        {
            throw new NotImplementedException();
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
                .ToListAsync();
        }

        public Task<CommentInfoModel> GetLastCommentAboutRecipeAsync(int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
