using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("task")]
public class Task
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
    [ForeignKey(nameof(Status))]
    [Column("status_id")]
    public long StatusId { get; set; }
    public TaskStatus Status { get; set; } = null!;

    [Required, MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [Required]
    [Column("event_time")]
    public DateTime EventTime { get; set; }

    [Required]
    [Column("is_deadline")]
    public bool IsDeadline { get; set; }

    [Required, MaxLength(2000)]
    [Column("description")]
    public string Description { get; set; } = null!;
}