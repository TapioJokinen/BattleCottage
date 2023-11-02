using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class LFGPostGameRole : BaseEntity
    {
        [Required]
        public int LFGPostId { get; set; }
        public LFGPost LFGPost { get; set; } = null!;

        [Required]
        public int GameRoleId { get; set; }
        public GameRole GameRole { get; set; } = null!;
    }
}
