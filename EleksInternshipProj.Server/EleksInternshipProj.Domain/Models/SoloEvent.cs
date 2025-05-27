using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("soloevent")]
public class SoloEvent
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [ForeignKey(nameof(Event))]
    [Column("event_id")]
    public long EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Required]
    [Column("event_time")]
    public DateTime EventTime { get; set; }
}