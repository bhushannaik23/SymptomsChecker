using System.ComponentModel.DataAnnotations;

namespace Symptoms_Checker.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime DateTime { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";
    }
}
