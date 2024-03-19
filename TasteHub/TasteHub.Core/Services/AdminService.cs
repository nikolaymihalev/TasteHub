using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.Admin;
using TasteHub.Core.Models.User;
using TasteHub.Infrastructure.Common;
using TasteHub.Infrastructure.Data.Models;

namespace TasteHub.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository repository;

        public AdminService(IRepository _repository)
        {
            repository = _repository;
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

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return await repository.AllReadonly<IdentityUser>()
                .Select(x => new UserViewModel(
                    x.Id,
                    x.UserName))
                .ToListAsync();
        }
    }
}
