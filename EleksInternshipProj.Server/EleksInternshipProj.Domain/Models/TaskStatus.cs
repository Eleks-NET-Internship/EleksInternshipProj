using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("taskstatus")]
public class TaskStatus
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required, MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = null!;

    public List<TaskModel> Tasks { get; set; } = new();
}