using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleksInternshipProj.Domain.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required, MaxLength(255)]
        [Column("user_name")]
        public string UserName { get; set; } = null!;

        [MaxLength(255)]
        [Column("first_name")]
        public string? FirstName { get; set; }

        [MaxLength(255)]
        [Column("last_name")]
        public string? LastName { get; set; }

        [Required, MaxLength(255)]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("password_hash")]
        public byte[]? PasswordHash { get; set; }

        [Column("password_salt")]
        public byte[]? PasswordSalt { get; set; }

        [Required, MaxLength(50)]
        [Column("auth_provider")]
        public string AuthProvider { get; set; } = null!;

        [MaxLength(255)]
        [Column("external_id")]
        public string? ExternalId {  get; set; }

        public List<UserSpace> UserSpaces { get; set; } = new();
    }
}
