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

    [Required]
    [Column("start_time")]
    public TimeOnly StartTime { get; set; }

    [Required]
    [Column("end_time")]
    public TimeOnly EndTime { get; set; }

    public List<EventMarker> EventMarkers { get; set; } = new();
    public List<EventTimetableDay> EventTimetableDays { get; set; } = new();
    public List<SoloEvent> SoloEvents { get; set; } = new();
    public List<Note> Notes { get; set; } = new();
    public List<Task> Tasks { get; set; } = new();
}