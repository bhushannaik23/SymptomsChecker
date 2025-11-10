using System.ComponentModel.DataAnnotations;

namespace Symptoms_Checker.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Phone]
        public string Phone { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<PatientHistory> PatientHistories { get; set; }
    }
}
