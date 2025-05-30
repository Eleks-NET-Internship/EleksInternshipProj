using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("event_timetable_day")]
public class EventTimetableDay
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
    [ForeignKey(nameof(TimetableDay))]
    [Column("timetable_day_id")]
    public long TimetableDayId { get; set; }
    public TimetableDay TimetableDay { get; set; } = null!;

    [Required]
    [Column("start_time")]
    public TimeOnly StartTime { get; set; }

    [Required]
    [Column("end_time")]
    public TimeOnly EndTime { get; set; }
}