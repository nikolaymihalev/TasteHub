using TasteHub.Core.Models.Comment;

namespace TasteHub.Core.Contracts
{
    public interface ICommentService
    {
        Task AddSync(CommentFormModel model);
        Task DeleteAsync(int id);
        Task<CommentInfoModel> GetByIdAsync(int id); 
        Task<IEnumerable<CommentInfoModel>> GetAllCommentsAboutRecipeAsync(int recipeId);
        Task<CommentInfoModel?> GetLastCommentAboutRecipeAsync(int recipeId);
        void DeleteRange(IEnumerable<CommentInfoModel> models);
    }
}
