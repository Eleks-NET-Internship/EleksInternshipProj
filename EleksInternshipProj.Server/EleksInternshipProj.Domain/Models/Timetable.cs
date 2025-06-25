using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("timetable")]
public class Timetable
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [ForeignKey(nameof(Space))]
    [Column("space_id")]
    public long SpaceId { get; set; }
    public Space Space { get; set; } = null!;

    public List<Day> Days { get; set; } = new();
}