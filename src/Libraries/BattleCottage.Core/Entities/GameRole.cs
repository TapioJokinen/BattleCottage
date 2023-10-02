using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class GameRole : BaseEntity
    {
        [Required]
        public required string Name { get; set; }

        public List<LfgPostGameRole> LfgPostGameRoles { get; } = new();
    }
}