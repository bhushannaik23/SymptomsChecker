using System.ComponentModel.DataAnnotations;

namespace Symptoms_Checker.Models
{
    public class PatientHistory
    {
        [Key]
        public int HistoryId { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public string SymptomsText { get; set; }

        public string DiseaseSuggested { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
