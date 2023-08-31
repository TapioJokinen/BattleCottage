using BattleCottage.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BattleCottage.Data.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IdentityResult> AddUserAsync(User user, string? password);

        Task<IdentityResult> UpdateUserAsync(User entity);

        Task<User?> FindByEmailAsync(string? email);

        Task<IList<string>> GetUserRolesAsync(User user);

        Task<bool> CheckPasswordAsync(User user, string? password);

        Task<IList<string>> ValidatePasswordAsync(string? password);
    }
}
