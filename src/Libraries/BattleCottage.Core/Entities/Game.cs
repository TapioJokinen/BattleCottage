namespace BattleCottage.Core.Entities
{
    public class Game : BaseEntity
    {
        public required string Name { get; set; }
        public string? BackgroundImage { get; set; }
        public ICollection<LFGPost>? LFGPosts { get; }
    }
}
