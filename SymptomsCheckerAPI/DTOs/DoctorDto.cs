using System.Text.Json.Serialization;

namespace Symptoms_Checker.DTOs
{
    public class DoctorDto
    {
        [JsonPropertyName("doctorId")]
        public int DoctorId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; }
    }

    public class CreateDoctorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }

    public class UpdateDoctorDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<int> SpecialtyIds { get; set; } = new List<int>();
    }
}
