using TasteHub.Core.Models;

namespace TasteHub.Core.Contracts
{
    public interface ICommentService
    {
        Task AddSync(CommentFormModel model);
        Task DeleteAsync(int id);
        Task<IEnumerable<CommentInfoModel>> GetAllCommentsAboutRecipe();
        Task<CommentInfoModel> GetLastCommentAboutRecipe();
    }
}
