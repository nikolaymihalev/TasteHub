namespace TasteHub.Core.Contracts
{
    public interface ICommentService
    {
        Task AddSync();
        Task DeleteAsync();
        Task GetAllCommentsAboutRecipe();
        Task GetLastCommentAboutRecipe();
    }
}
