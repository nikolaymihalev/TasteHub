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

        public async Task<IEnumerable<RoleInfoModel>> GetAllRolesAsync()
        {
            return await repository.AllReadonly<IdentityRole>()
                .Select(x => new RoleInfoModel(
                    x.Id,
                    x.Name))
                .ToListAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return await repository.AllReadonly<IdentityUser>()
                .Select(x => new UserViewModel(
                    x.Id,
                    x.UserName))
                .ToListAsync();
        }

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

        public async Task<bool> UserExistsAsync(string userId)
        {
            return await repository.AllReadonly<AdminQuery>()
                .AnyAsync(x => x.UserId == userId);
        }
    }
}
