using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("day")]
public class Day
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required, MaxLength(50)]
    [Column("day_name")]
    public string DayName { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Timetable))]
    [Column("timetable_id")]
    public long TimetableId { get; set; }
    public Timetable Timetable { get; set; } = null!;

    public List<EventDay> EventDays { get; set; } = new();
}