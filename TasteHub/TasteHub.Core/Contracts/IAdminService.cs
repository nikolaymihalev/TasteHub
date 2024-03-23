using TasteHub.Core.Models.Admin;
using TasteHub.Core.Models.User;

namespace TasteHub.Core.Contracts
{
    /// <summary>
    /// Interface for Admin service
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Get all existing users 
        /// </summary>
        /// <returns>Collection of User models</returns>
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        /// <summary>
        /// Get all queries from users to become admins
        /// </summary>
        /// <returns>Collection of Query models</returns>
        Task<IEnumerable<QueryInfoModel>> GetAllQueriesAsync();

        /// <summary>
        /// Get all existing roles
        /// </summary>
        /// <returns>Collection of Role models</returns>
        Task<IEnumerable<RoleInfoModel>> GetAllRolesAsync();

        /// <summary>
        /// Add query 
        /// </summary>
        /// <param name="model">Query model</param>
        Task AddAsync(QueryFormModel model);

        /// <summary>
        /// Remove query
        /// </summary>
        /// <param name="id">Query identifier</param>
        Task RemoveAsync(int id);

        /// <summary>
        /// Check if user exists by identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>True or False</returns>
        Task<bool> UserExistsAsync(string userId);

        /// <summary>
        /// Check if query exists by identifier
        /// </summary>
        /// <param name="id">Query identifier</param>
        /// <returns>True or False</returns>
        Task<bool> QueryExistsAsync(int id);
    }
}
