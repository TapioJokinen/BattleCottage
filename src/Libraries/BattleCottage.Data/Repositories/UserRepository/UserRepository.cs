using BattleCottage.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BattleCottage.Data.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string? password)
        {
            if (string.IsNullOrEmpty(password)) return IdentityResult.Failed();

            IdentityResult result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<bool> CheckPasswordAsync(User user, string? password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (password == null) throw new ArgumentNullException(nameof(password));

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<User?> FindByEmailAsync(string? email)
        {
            User? user = await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<ICollection<string>> GetUserRolesAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(User entity)
        {
            return await _userManager.UpdateAsync(entity);
        }

        public async Task<ICollection<string>> ValidatePasswordAsync(string? password)
        {
            IList<string> passwordErrors = new List<string>();

            foreach (var validator in _userManager.PasswordValidators)
            {
#pragma warning disable CS8625 // This is a valid way to check.
                IdentityResult result = await validator.ValidateAsync(_userManager, null, password);
#pragma warning restore CS8625

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        passwordErrors.Add(error.Description);
                    }
                }
            }

            return passwordErrors;
        }
    }
}
