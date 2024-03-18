using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TasteHub.Core.Contracts;
using TasteHub.Core.Models.User;
using TasteHub.Infrastructure.Common;

namespace TasteHub.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository _repository)
        {
            repository = _repository;
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
