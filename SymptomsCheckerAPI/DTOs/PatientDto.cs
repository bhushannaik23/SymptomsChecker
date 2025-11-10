namespace Symptoms_Checker.DTOs
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class CreatePatientDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }

    public class UpdatePatientDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
