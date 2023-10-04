using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class GameMode : BaseEntity
    {
        [Required]
        public required string Name { get; set; }

        public ICollection<LFGPost>? LFGPosts { get; }
    }
}