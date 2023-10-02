using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class Game : BaseEntity
    {
        [Required]
        public required string Name { get; set; }

        public string? BackgroundImage { get; set; }

        public ICollection<LfgPost>? LfgPosts { get; }
    }
}
