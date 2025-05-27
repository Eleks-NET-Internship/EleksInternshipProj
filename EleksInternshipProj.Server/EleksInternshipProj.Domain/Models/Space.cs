using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("space")]
public class Space
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required, MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = null!;

    public List<UserSpace> UserSpaces { get; set; } = new();
    public List<Marker> Markers { get; set; } = new();
    public List<Timetable> Timetables { get; set; } = new();
}