using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("timetable_day")]
public class TimetableDay
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [ForeignKey(nameof(Timetable))]
    [Column("timetable_id")]
    public long TimetableId { get; set; }
    public Timetable Timetable { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Day))]
    [Column("day_id")]
    public long DayId { get; set; }
    public Day Day { get; set; } = null!;

    public List<EventTimetableDay> EventTimetableDays { get; set; } = new();
}
