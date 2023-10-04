using BattleCottage.Core.Entities;
using BattleCottage.Core.Exceptions;
using BattleCottage.Data;
using BattleCottage.Data.Repositories;
using BattleCottage.Services.LFGPosts.Constants;
using Microsoft.EntityFrameworkCore;

namespace BattleCottage.Services.LFGPosts
{
    public class LFGPostService : ILFGPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<GameMode> _gameModeRepository;
        private readonly IRepository<GameStyle> _gameStyleRepository;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<LFGPost> _LFGPostRepository;
        private readonly IRepository<GameRole> _gameRoleRepository;
        private readonly IRepository<LFGPostGameRole> _LFGPostGameRoleRepository;

        public LFGPostService(ApplicationDbContext context,
            IRepository<GameMode> gameModeRepository,
            IRepository<Game> gameRepository,
            IRepository<GameStyle> gameStyleRepository,
            IRepository<GameRole> gameRoleRepository,
            IRepository<LFGPost> LFGPostRepository,
            IRepository<LFGPostGameRole> LFGPostGameRoleRepository
        )
        {
            _context = context;
            _gameRepository = gameRepository;
            _gameModeRepository = gameModeRepository;
            _gameStyleRepository = gameStyleRepository;
            _gameRoleRepository = gameRoleRepository;
            _LFGPostRepository = LFGPostRepository;
            _LFGPostGameRoleRepository = LFGPostGameRoleRepository;
        }

        public async Task<LFGPost> CreateLFGPost(User user, LFGPostFormInput formInput)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await LFGPostFormInputValidator(formInput);


            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var lfgPost = new LFGPost
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
                await _LFGPostRepository.AddAsync(lfgPost);
                await _context.SaveChangesAsync();

                var lfgPostGameRoles = formInput.GameRoleIds?.Select(gameRoleId => new LFGPostGameRole
                {
                    GameRoleId = gameRoleId,
                    LFGPostId = lfgPost.Id,
                    DateAdded = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                });

                await _LFGPostGameRoleRepository.AddRangeAsync(lfgPostGameRoles ?? throw new ArgumentNullException(nameof(lfgPostGameRoles)));
                await _context.SaveChangesAsync();

                transaction.Commit();
                return lfgPost;
            }
            catch (Exception)
            {
                throw new DbUpdateException("An error occurred while creating the LFG post.");
            }

        }

        public async Task LFGPostFormInputValidator(LFGPostFormInput formInput)
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