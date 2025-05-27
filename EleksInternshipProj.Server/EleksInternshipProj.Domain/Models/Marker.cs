using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("marker")]
public class Marker
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required, MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Space))]
    [Column("space_id")]
    public long SpaceId { get; set; }
    public Space Space { get; set; } = null!;

    public List<EventMarker> EventMarkers { get; set; } = new();
}