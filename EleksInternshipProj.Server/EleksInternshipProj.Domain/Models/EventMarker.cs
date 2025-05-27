using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("event_marker")]
public class EventMarker
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
    [ForeignKey(nameof(Marker))]
    [Column("marker_id")]
    public long MarkerId { get; set; }
    public Marker Marker { get; set; } = null!;
}