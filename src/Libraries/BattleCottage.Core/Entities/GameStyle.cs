using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class GameStyle : BaseEntity
    {
        [Required]
        public required string Name { get; set; }

        public ICollection<LFGPost>? LFGPosts { get; }
    }
}