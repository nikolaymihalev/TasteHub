using TasteHub.Core.Models.User;

namespace TasteHub.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    }
}
