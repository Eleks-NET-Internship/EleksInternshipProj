using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;

[Table("user_space")]
public class UserSpace
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    [Column("user_id")]
    public long UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Space))]
    [Column("space_id")]
    public long SpaceId { get; set; }
    public Space Space { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Role))]
    [Column("role_id")]
    public long RoleId { get; set; }
    public Role Role { get; set; } = null!;
}