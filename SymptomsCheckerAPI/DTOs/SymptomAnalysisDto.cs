using System.Text.Json.Serialization;

namespace Symptoms_Checker.DTOs
{
    public class SymptomAnalysisRequestDto
    {
        [JsonPropertyName("patientId")]
        public int PatientId { get; set; }

        [JsonPropertyName("symptomsText")]
        public string SymptomsText { get; set; }
    }

    public class SymptomAnalysisResponseDto
    {
        [JsonPropertyName("predictedDisease")]
        public string PredictedDisease { get; set; }

        [JsonPropertyName("cause")]
        public string Cause { get; set; }

        [JsonPropertyName("cure")]
        public string Cure { get; set; }

        [JsonPropertyName("recommendedSpecialty")]
        public string RecommendedSpecialty { get; set; }

        [JsonPropertyName("recommendedDoctors")]
        public List<DoctorDto> RecommendedDoctors { get; set; }
    }
}
