using BattleCottage.Core.Entities;

namespace BattleCottage.Services.LFGPosts
{
    public interface ILFGPostService
    {
        Task LFGPostFormInputValidator(LFGPostFormInput formInput);

        Task<LFGPost> CreateLFGPost(User user, LFGPostFormInput formInput);

        Task<LFGPostFormOptions> GetLFGPostFormOptions();
    }
}
