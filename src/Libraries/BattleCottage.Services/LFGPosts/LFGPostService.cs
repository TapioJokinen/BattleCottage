using BattleCottage.Core.Entities;
using BattleCottage.Core.Exceptions;
using BattleCottage.Data;
using BattleCottage.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BattleCottage.Services.LFGPosts
{
    public class LFGPostService : ILFGPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<GameMode> _gameModeRepository;
        private readonly IRepository<GameStyle> _gameStyleRepository;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<LFGPost> _lfgPostRepository;
        private readonly IRepository<GameRole> _gameRoleRepository;
        private readonly IRepository<LFGPostGameRole> _lfgPostGameRoleRepository;
        private readonly IRepository<LFGPostDuration> _lfgPostDurationRepository;

        public LFGPostService(
            ApplicationDbContext context,
            IRepository<GameMode> gameModeRepository,
            IRepository<Game> gameRepository,
            IRepository<GameStyle> gameStyleRepository,
            IRepository<GameRole> gameRoleRepository,
            IRepository<LFGPost> lfgPostRepository,
            IRepository<LFGPostGameRole> lfgPostGameRoleRepository,
            IRepository<LFGPostDuration> lfgPostDurationRepository
        )
        {
            _context = context;
            _gameRepository = gameRepository;
            _gameModeRepository = gameModeRepository;
            _gameStyleRepository = gameStyleRepository;
            _gameRoleRepository = gameRoleRepository;
            _lfgPostRepository = lfgPostRepository;
            _lfgPostGameRoleRepository = lfgPostGameRoleRepository;
            _lfgPostDurationRepository = lfgPostDurationRepository;
        }

        /// <summary>
        /// Creates a new LFG post for the specified user with the given form input.
        /// </summary>
        /// <param name="user">The user creating the LFG post.</param>
        /// <param name="formInput">The form input containing the details of the LFG post.</param>
        /// <returns>The newly created LFG post.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user or form input is null.</exception>
        /// <exception cref="ObjectNotFoundException">Thrown when form value is not found from the database.</exception>
        /// <exception cref="DbUpdateException">Thrown when an error occurs while creating the LFG post.</exception>
        public async Task<LFGPost> CreateLFGPost(User user, LFGPostFormInput formInput)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await LFGPostFormInputValidator(formInput);

            // TODO: Make sure this is how you handle transactions.
            // (Tests!)
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var lfgPost = new LFGPost
                {
                    UserId = user.Id,
                    Title = formInput.Title ?? throw new ArgumentNullException(nameof(formInput.Title), "Title"),
                    Description =
                        formInput.Description
                        ?? throw new ArgumentNullException(nameof(formInput.Description), "Description"),
                    DurationInMinutesId = formInput.DurationId,
                    GameId = formInput.GameId,
                    GameModeId = formInput.GameModeId,
                    GameStyleId = formInput.GameStyleId,
                    DateAdded = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                await _lfgPostRepository.AddAsync(lfgPost);
                await _context.SaveChangesAsync();

                var lfgPostGameRoles =
                    (
                        formInput.GameRoleIds?.Select(
                            gameRoleId =>
                                new LFGPostGameRole
                                {
                                    GameRoleId = gameRoleId,
                                    LFGPostId = lfgPost.Id,
                                    DateAdded = DateTime.UtcNow,
                                    DateUpdated = DateTime.UtcNow
                                }
                        )
                    ) ?? throw new ArgumentNullException(nameof(formInput.GameRoleIds), "GameRoleIds");

                await _lfgPostGameRoleRepository.AddRangeAsync(lfgPostGameRoles);
                await _context.SaveChangesAsync();

                transaction.Commit();

                return lfgPost;
            }
            catch (Exception)
            {
                throw new DbUpdateException("An error occurred while creating the LFG post.");
            }
        }

        /// <summary>
        /// Validates the input for a new LFG post form.
        /// </summary>
        /// <param name="formInput">The input to validate.</param>
        /// <exception cref="ArgumentException">Thrown when the title or description is null or less than 3 characters long, or when no game roles are selected.</exception>
        /// <exception cref="ObjectNotFoundException">Thrown when the given duration, game, game mode, or game style is not found, or when one or more selected game roles are not found.</exception>
        public async Task LFGPostFormInputValidator(LFGPostFormInput formInput)
        {
            if (formInput.Title == null || formInput.Title.Length < 3)
            {
                throw new ArgumentException("Title must be at least 3 characters long.");
            }

            if (formInput.Description == null || formInput.Description.Length < 3)
            {
                throw new ArgumentException("Description must be at least 3 characters long.");
            }

            if (await _lfgPostDurationRepository.FindByIdAsync(formInput.DurationId) == null)
            {
                throw new ObjectNotFoundException("Given duration not found.");
            }

            if (await _gameRepository.FindByIdAsync(formInput.GameId) == null)
            {
                throw new ObjectNotFoundException("Given game not found.");
            }

            if (await _gameModeRepository.FindByIdAsync(formInput.GameModeId) == null)
            {
                throw new ObjectNotFoundException("Given game mode not found.");
            }

            if (await _gameStyleRepository.FindByIdAsync(formInput.GameStyleId) == null)
            {
                throw new ObjectNotFoundException("Given game style not found.");
            }

            if (formInput.GameRoleIds == null || formInput.GameRoleIds.Length == 0)
            {
                throw new ArgumentException("At least one game role must be selected.");
            }

            var gameRoles = await _gameRoleRepository.Filter(x => formInput.GameRoleIds.Contains(x.Id));

            if (gameRoles == null || gameRoles.Count != formInput.GameRoleIds.Distinct().Count())
            {
                throw new ObjectNotFoundException("One or more game roles were not found.");
            }
        }

        /// <summary>
        /// Retrieves the options needed to populate the LFG post creation form.
        /// </summary>
        /// <returns>An instance of LFGPostFormOptions containing the available game modes, styles, roles, and durations.</returns>
        public async Task<LFGPostFormOptions> GetLFGPostFormOptions()
        {
            IList<GameMode>? gameModes = await _gameModeRepository.GetAllAsync();
            IList<GameStyle>? gameStyles = await _gameStyleRepository.GetAllAsync();
            IList<GameRole>? gameRoles = await _gameRoleRepository.GetAllAsync();
            IList<LFGPostDuration>? lfgPostDurations = await _lfgPostDurationRepository.GetAllAsync();

            return new LFGPostFormOptions
            {
                GameModes = gameModes,
                GameStyles = gameStyles,
                GameRoles = gameRoles,
                LFGPostDurations = lfgPostDurations
            };
        }
    }
}
