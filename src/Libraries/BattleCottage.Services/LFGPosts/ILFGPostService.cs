using BattleCottage.Core.Entities;

namespace BattleCottage.Services.LFGPosts
{
    public interface ILFGPostService
    {
        void LFGPostFormInputValidator(LFGPostFormInput formInput);
        Task<LFGPost> CreateLFGPost(User user, LFGPostFormInput formInput);
    }
}