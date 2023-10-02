using BattleCottage.Core.Entities;

namespace BattleCottage.Services.LfgPosts
{
    public interface ILfgPostService
    {
        Task LfgPostFormInputValidator(LfgPostFormInput formInput);
        Task<LfgPost> CreateLfgPost(User user, LfgPostFormInput formInput);
    }
}