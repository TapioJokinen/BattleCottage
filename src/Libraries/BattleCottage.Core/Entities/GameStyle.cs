namespace BattleCottage.Core.Entities
{
    public class GameStyle : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<LFGPost>? LFGPosts { get; }
    }
}