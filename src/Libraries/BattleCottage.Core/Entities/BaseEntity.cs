using System.ComponentModel.DataAnnotations;

namespace BattleCottage.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
}
