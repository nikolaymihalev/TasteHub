using TasteHub.Core.Models.Admin;
using TasteHub.Core.Models.User;

namespace TasteHub.Core.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<IEnumerable<QueryInfoModel>> GetAllQueriesAsync();
    }
}
