using BattleCottage.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException("`AddAsync` not available for this repository. Use `AddAsyncWithPassword` instead.");
        }

        /// <summary>
        /// Asynchronoysly creates a user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AddUserAsync(User user, string? password)
        {
            if (string.IsNullOrEmpty(password)) return IdentityResult.Failed();

            IdentityResult result = await _userManager.CreateAsync(user, password);

            return result;
        }

        /// <summary>
        /// Checks if the given password matches for the user's password in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> CheckPasswordAsync(User user, string? password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (password == null) throw new ArgumentNullException(nameof(password));

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task<User> DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User?>> Filter(Expression<Func<User, bool>> filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously finds the user by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User?> FindByEmailAsync(string? email)
        {
            User? user = await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return user;
        }

        public Task<User> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously gets user roles.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userManager.GetRolesAsync(user);
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously validates the given password for errors.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IList<string>> ValidatePasswordAsync(string? password)
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
