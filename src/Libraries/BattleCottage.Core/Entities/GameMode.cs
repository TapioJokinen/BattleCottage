namespace BattleCottage.Core.Entities
{
    public class GameMode : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<LFGPost>? LFGPosts { get; }
    }
}