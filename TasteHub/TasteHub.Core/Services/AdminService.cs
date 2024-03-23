using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Admin;
using TasteHub.Core.Models.User;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Constants;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    /// <summary>
    /// Service for Admin
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly IRepository repository;
        private readonly ILogger<AdminService> logger;

        public AdminService(
            IRepository _repository,
            ILogger<AdminService> _logger)
        {
            repository = _repository;
            logger = _logger;
        }

        /// <summary>
        /// Add query for becoming admin
        /// </summary>
        /// <param name="model">Query model</param>
        /// <exception cref="ApplicationException">Operation is failed</exception>
        public async Task AddAsync(QueryFormModel model)
        {
            var entity = new AdminQuery()
            {                
                UserId = model.UserId,
                Description = model.Description,
            };

            try
            {
                await repository.AddAsync<AdminQuery>(entity);
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "AdminService.AddAsync");
                throw new ApplicationException(ErrorMessageConstants.OperationFailedErrorMessage);
            }
        }

        /// <summary>
        /// Get all queries from users
        /// </summary>
        /// <returns>Collection of Query models</returns>
        public async Task<IEnumerable<QueryInfoModel>> GetAllQueriesAsync()
        {
            return await repository.AllReadonly<AdminQuery>()
                .Select(x=>new QueryInfoModel(
                    x.Id,
                    x.UserId,
                    x.User.UserName,
                    x.Description))
                .ToListAsync();
        }

        /// <summary>
        /// Get all existing roles
        /// </summary>
        /// <returns>Collection of Role models</returns>
        public async Task<IEnumerable<RoleInfoModel>> GetAllRolesAsync()
        {
            return await repository.AllReadonly<IdentityRole>()
                .Select(x => new RoleInfoModel(
                    x.Id,
                    x.Name))
                .ToListAsync();
        }

        /// <summary>
        /// Get all existing users 
        /// </summary>
        /// <returns>Collection of User models</returns>
        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return await repository.AllReadonly<IdentityUser>()
                .Select(x => new UserViewModel(
                    x.Id,
                    x.UserName))
                .ToListAsync();
        }

        /// <summary>
        /// Check if query exists by identifier
        /// </summary>
        /// <param name="id">Query identifier</param>
        /// <returns>True or False</returns>
        public async Task<bool> QueryExistsAsync(int id)
        {
            return await repository.AllReadonly<AdminQuery>()
                .AnyAsync(x => x.Id == id);             
        }

        /// <summary>
        /// Remove query
        /// </summary>
        /// <param name="id">Query identifier</param>
        /// <exception cref="ApplicationException">Invalid model</exception>
        public async Task RemoveAsync(int id)
        {
            var query = await repository.GetByIdAsync<AdminQuery>(id);

            if (query == null)
            {
                throw new ApplicationException(string.Format(ErrorMessageConstants.InvalidModelErrorMessage, "query"));
            }

            await repository.DeleteAsync<AdminQuery>(query.Id);

            await repository.SaveChangesAsync();
        }

        /// <summary>
        /// Check if user exists by identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>True or False</returns>
        public async Task<bool> UserExistsAsync(string userId)
        {
            return await repository.AllReadonly<AdminQuery>()
                .AnyAsync(x => x.UserId == userId);
        }
    }
}
