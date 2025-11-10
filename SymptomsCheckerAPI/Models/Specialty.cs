using System.ComponentModel.DataAnnotations;

namespace Symptoms_Checker.Models
{
    public class Specialty
    {
        [Key]
        public int SpecialtyId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }
    }
}
