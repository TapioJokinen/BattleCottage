using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public class LFGPost : BaseEntity
    {
        [Required]
        public required string UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public int DurationInMinutes { get; set; }

        [Required]
        public int GameModeId { get; set; }
        public GameMode GameMode { get; set; } = null!;

        [Required]
        public int GameStyleId { get; set; }
        public GameStyle GameStyle { get; set; } = null!;

        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public List<LFGPostGameRole>? LFGPostGameRoles { get; }
    }
}