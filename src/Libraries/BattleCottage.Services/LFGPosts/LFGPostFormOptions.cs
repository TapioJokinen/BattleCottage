using BattleCottage.Core.Entities;

namespace BattleCottage.Services.LFGPosts
{
    public class LFGPostFormOptions
    {
        public IList<GameMode>? GameModes { get; set; }
        public IList<GameStyle>? GameStyles { get; set; }
        public IList<GameRole>? GameRoles { get; set; }
        public IList<LFGPostDuration>? LFGPostDurations { get; set; }
    }
}
