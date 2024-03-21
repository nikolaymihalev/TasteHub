using TasteHub.Core.Models.Admin;
using TasteHub.Core.Models.User;

namespace TasteHub.Core.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<IEnumerable<QueryInfoModel>> GetAllQueriesAsync();
        Task<IEnumerable<RoleInfoModel>> GetAllRolesAsync();
        Task AddAsync(QueryFormModel model);
        Task RemoveAsync(int id);
        Task<bool> UserExistsAsync(string userId);
        Task<QueryInfoModel> GetByIdAsync(int id);
    }
}
