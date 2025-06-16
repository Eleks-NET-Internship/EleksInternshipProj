using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EleksInternshipProj.Domain.Models
{
    public class Notification
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("space_id")]
        public long SpaceId { get; set; }
        [Required]
        [ForeignKey(nameof(SpaceId))]
        public Space Space { get; set; } = null!;

        [Required]
        [Column("related_type")]
        public string RelatedType { get; set; } = null!;

        [Required]
        [Column("related_id")]
        public long RelatedId { get; set; }

        [Required, MaxLength(32)]
        [Column("title")]
        public string Title { get; set; } = null!;

        [Required, MaxLength(255)]
        [Column("message")]
        public string Message { get; set; } = null!;

        [Required]
        [Column("sent_at")]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
