using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("role")]
public class Role
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required, MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = null!;

    public List<UserSpace> UserSpaces { get; set; } = new();
}