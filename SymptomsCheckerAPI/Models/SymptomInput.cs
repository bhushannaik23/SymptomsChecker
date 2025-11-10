using System.ComponentModel.DataAnnotations;

namespace Symptoms_Checker.Models
{
    public class SymptomInput
    {
        [Key]
        public int SymptomInputId { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public string SymptomsText { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
