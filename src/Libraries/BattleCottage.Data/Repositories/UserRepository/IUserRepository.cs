using Microsoft.AspNetCore.Identity;
using BattleCottage.Core.Entities;

namespace BattleCottage.Data.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IdentityResult> AddUserAsync(User user, string? password);

        Task<User?> FindByEmailAsync(string? email);

        Task<IList<string>> GetUserRolesAsync(User user);

        Task<bool> CheckPasswordAsync(User user, string? password);

        Task<IList<string>> ValidatePasswordAsync(string? password);
    }
}
