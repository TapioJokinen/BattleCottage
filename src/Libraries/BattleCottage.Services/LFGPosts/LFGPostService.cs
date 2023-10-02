using BattleCottage.Core.Entities;
using BattleCottage.Core.Exceptions;
using BattleCottage.Data;
using BattleCottage.Data.Repositories;
using BattleCottage.Services.LfgPosts.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BattleCottage.Services.LfgPosts
{
    public class LfgPostService : ILfgPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<GameMode> _gameModeRepository;
        private readonly IRepository<GameStyle> _gameStyleRepository;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<LfgPost> _lfgPostRepository;
        private readonly IRepository<GameRole> _gameRoleRepository;
        private readonly IRepository<LfgPostGameRole> _lfgPostGameRoleRepository;

        public LfgPostService(ApplicationDbContext context,
            IRepository<GameMode> gameModeRepository,
            IRepository<Game> gameRepository,
            IRepository<GameStyle> gameStyleRepository,
            IRepository<GameRole> gameRoleRepository,
            IRepository<LfgPost> LfgPostRepository,
            IRepository<LfgPostGameRole> LfgPostGameRoleRepository
        )
        {
            _context = context;
            _gameRepository = gameRepository;
            _gameModeRepository = gameModeRepository;
            _gameStyleRepository = gameStyleRepository;
            _gameRoleRepository = gameRoleRepository;
            _lfgPostRepository = LfgPostRepository;
            _lfgPostGameRoleRepository = LfgPostGameRoleRepository;
        }

        public async Task<LfgPost> CreateLfgPost(User user, LfgPostFormInput formInput)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await LfgPostFormInputValidator(formInput);


            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var lfgPost = new LfgPost
                {
                    UserId = user.Id,
                    Title = formInput.Title ?? throw new ArgumentNullException(nameof(formInput.Title)),
                    Description = formInput.Description ?? throw new ArgumentNullException(nameof(formInput.Description)),
                    DurationInMinutes = formInput.Duration,
                    GameId = formInput.GameId,
                    GameModeId = formInput.GameModeId,
                    GameStyleId = formInput.GameStyleId,
                    DateAdded = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };
                await _lfgPostRepository.AddAsync(lfgPost);
                await _context.SaveChangesAsync();

                var LfgPostGameRoles = formInput.GameRoleIds?.Select(gameRoleId => new LfgPostGameRole
                {
                    GameRoleId = gameRoleId,
                    LfgPostId = lfgPost.Id,
                    DateAdded = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                });

                await _lfgPostGameRoleRepository.AddRangeAsync(LfgPostGameRoles ?? throw new ArgumentNullException(nameof(LfgPostGameRoles)));
                await _context.SaveChangesAsync();

                transaction.Commit();
                return lfgPost;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("An error occurred while creating the LFG post.");
            }

        }

        public async Task LfgPostFormInputValidator(LfgPostFormInput formInput)
        {
            var validDurationsInMinutes = new int[] {
                (int)Durations.OneHour,
                (int)Durations.FiveHours,
                (int)Durations.OneDay,
                (int)Durations.OneWeek,
                (int)Durations.ThirtyDays
            };

            if (formInput.Title == null || formInput.Title.Length < 3)
            {
                throw new ArgumentException("Title must be at least 3 characters long.", nameof(formInput.Title));
            }

            if (formInput.Description == null || formInput.Description.Length < 3)
            {
                throw new ArgumentException("Description must be at least 3 characters long.", nameof(formInput.Description));
            }

            if (!validDurationsInMinutes.Contains(formInput.Duration))
            {
                throw new ArgumentException("Duration must be one of the following: 60, 300, 1440, 10080, 43200.", nameof(formInput.Duration));
            }

            if (await _gameRepository.FindByIdAsync(formInput.GameId) == null)
            {
                throw new ObjectNotFoundException($"No game found.");
            }

            if (await _gameModeRepository.FindByIdAsync(formInput.GameModeId) == null)
            {
                throw new ObjectNotFoundException($"No game mode found.");
            }

            if (await _gameStyleRepository.FindByIdAsync(formInput.GameStyleId) == null)
            {
                throw new ObjectNotFoundException($"No game style found.");
            }

            if (formInput.GameRoleIds == null || formInput.GameRoleIds.Length == 0)
            {
                throw new ArgumentException("At least one game role must be selected.", nameof(formInput.GameRoleIds));
            }

            var gameRoles = await _gameRoleRepository.Filter(x => formInput.GameRoleIds.Contains(x.Id));

            if (gameRoles == null || gameRoles.Count != formInput.GameRoleIds.Distinct().Count())
            {
                throw new ObjectNotFoundException($"One or more game roles were not found.");
            }
        }
    }
}