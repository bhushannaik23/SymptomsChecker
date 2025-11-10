using System.ComponentModel.DataAnnotations;

namespace Symptoms_Checker.Models
{
    public class DoctorSpecialty
    {
        [Key]
        public int DoctorSpecialtyId { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
    }
}
