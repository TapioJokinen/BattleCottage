using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class LFGPostDuration : BaseEntity
    {
        [Required]
        public required int DurationInMinutes { get; set; }

        [Required]
        public required string Name { get; set; }

        public ICollection<LFGPost>? LFGPosts { get; }
    }
}
