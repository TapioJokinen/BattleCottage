using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class GameRole : BaseEntity
    {
        [Required]
        public required string Name { get; set; }

        public List<LFGPostGameRole> LFGPostGameRoles { get; } = new();
    }
}
