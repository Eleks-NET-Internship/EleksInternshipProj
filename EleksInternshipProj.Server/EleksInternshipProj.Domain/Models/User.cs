using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Required, MaxLength(255)]
        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(255)]
        [Column("last_name")]
        public string LastName { get; set; } = null!;

        [Required, MaxLength(255)]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("password_hash")]
        public byte[] PasswordHash { get; set; } = null!;

        [Required]
        [Column("password_salt")]
        public byte[] PasswordSalt { get; set; } = null!;

        public List<UserSpace> UserSpaces { get; set; } = new();
    }
}
