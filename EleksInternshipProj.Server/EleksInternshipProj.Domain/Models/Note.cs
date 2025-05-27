using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models;


[Table("note")]
public class Note
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [ForeignKey(nameof(Event))]
    [Column("event_id")]
    public long EventId { get; set; }
    public Event Event { get; set; } = null!;

    [Required, MaxLength(255)]
    [Column("title")]
    public string Title { get; set; } = null!;

    [Required, MaxLength(2000)]
    [Column("content")]
    public string Content { get; set; } = null!;
}