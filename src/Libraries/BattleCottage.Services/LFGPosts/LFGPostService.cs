using BattleCottage.Core.Entities;
using BattleCottage.Data.Repositories.GameModeRepository;
using BattleCottage.Data.Repositories.GameStyleRepository;

namespace BattleCottage.Services.LFGPosts
{
    public class LFGPostService : ILFGPostService
    {
        private readonly IGameModeRepository _gameModeRepository;
        private readonly IGameStyleRepository _gameStyleRepository;

        public LFGPostService(IGameModeRepository gameModeRepository, IGameStyleRepository gameStyleRepository)
        {
            _gameModeRepository = gameModeRepository;
            _gameStyleRepository = gameStyleRepository;
        }

        public Task<LFGPost> CreateLFGPost(User user, LFGPostFormInput formInput)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            LFGPostFormInputValidator(formInput);

            return null;
        }

        public void LFGPostFormInputValidator(LFGPostFormInput formInput)
        {
            var validDurationsInMinutes = new int[] { 60, 300, 1440, 10080, 43200 };

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

            if (formInput.GameId == null)
            {
                throw new ArgumentNullException(nameof(formInput.GameId));
            }

            if (_gameModeRepository.FindByIdAsync(formInput.GameModeId) == null)
            {
                throw new ArgumentNullException(nameof(formInput.GameModeId));
            }

            if (_gameStyleRepository.FindByIdAsync(formInput.GameStyleId) == null)
            {
                throw new ArgumentNullException(nameof(formInput.GameStyleId));
            }
        }
    }
}