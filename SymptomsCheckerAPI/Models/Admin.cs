using System.ComponentModel.DataAnnotations;

namespace Symptoms_Checker.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
