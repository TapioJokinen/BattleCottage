using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class LfgPostGameRole : BaseEntity
    {
        [Required]
        public int LfgPostId { get; set; }
        public LfgPost LfgPost { get; set; } = null!;

        [Required]
        public int GameRoleId { get; set; }
        public GameRole GameRole { get; set; } = null!;
    }
}