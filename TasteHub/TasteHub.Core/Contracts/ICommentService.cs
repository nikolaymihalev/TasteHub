using TasteHub.Core.Models.Comment;

namespace TasteHub.Core.Contracts
{
    /// <summary>
    /// Interface for Comment service
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Add Comment
        /// </summary>
        /// <param name="model">Comment model</param>
        Task AddAsync(CommentFormModel model);

        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="id">Comment identifier</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get Comment by identifier
        /// </summary>
        /// <param name="id">Comment identifier</param>
        /// <returns>Comment model</returns>
        Task<CommentInfoModel> GetByIdAsync(int id); 

        /// <summary>
        /// Get all existing comments about recipe
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <returns>Collection of Comment models</returns>
        Task<IEnumerable<CommentInfoModel>> GetAllCommentsAboutRecipeAsync(int recipeId);

        /// <summary>
        /// Get last comment about recipe
        /// </summary>
        /// <param name="recipeId">Recipe identifier</param>
        /// <returns>Comment model or null</returns>
        Task<CommentInfoModel?> GetLastCommentAboutRecipeAsync(int recipeId);
    }
}
