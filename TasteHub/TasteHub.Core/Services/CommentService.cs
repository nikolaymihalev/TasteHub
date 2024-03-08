using TasteHub.Core.Contracts;
using TasteHub.Core.Models;

namespace TasteHub.Core.Services
{
    public class CommentService : ICommentService
    {
        public Task AddSync(CommentFormModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentInfoModel>> GetAllCommentsAboutRecipe()
        {
            throw new NotImplementedException();
        }

        public Task<CommentInfoModel> GetLastCommentAboutRecipe()
        {
            throw new NotImplementedException();
        }
    }
}
