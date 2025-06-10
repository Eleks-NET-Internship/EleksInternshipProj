using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("event")]
public class Event
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required, MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("is_solo")]
    public bool IsSolo { get; set; } = false;

    [Required]
    [ForeignKey(nameof(Space))]
    [Column("space_id")]
    public long SpaceId { get; set; }
    public Space Space { get; set; } = null!;

    public List<EventMarker> EventMarkers { get; set; } = new();
    public List<EventTimetableDay> EventTimetableDays { get; set; } = new();
    public List<SoloEvent> SoloEvents { get; set; } = new();
    public List<Note> Notes { get; set; } = new();
    public List<TaskModel> Tasks { get; set; } = new();
}

