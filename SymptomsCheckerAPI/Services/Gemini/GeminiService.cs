using System.Text;
using System.Text.Json;
using Symptoms_Checker.DTOs;
using Symptoms_Checker.Models;

namespace Symptoms_Checker.Services
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GeminiApi:ApiKey"] ?? "";
        }

        public async Task<SymptomAnalysisResponseDto> AnalyzeSymptomsAsync(string symptoms)
        {
            // If no API key -> fallback to mock
            if (string.IsNullOrEmpty(_apiKey))
                return GetMockResponse(symptoms);

            try
            {
                var prompt = $"Analyze these symptoms: {symptoms}\n\nRespond with a JSON object in this exact format:\n{{\n \"predicted_disease\",\n \"cause\": \"likely cause\",\n \"cure\": \"recommended treatment\",\n \"recommended_specialty\": \"medical specialty\"\n}}\n\nKeep responses concise and medical.";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
                            }
                        }
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(
                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}",
                    content
                );


                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return ParseGeminiResponse(responseContent);
                }
                else
                {
                    Console.WriteLine($"Gemini API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    
                }
            }
            catch
            {
              //fallback to mock if API fails.
            }
            return GetMockResponse(symptoms);
        }

        // ✅ MOCK response when API key is missing or API fails
        private SymptomAnalysisResponseDto GetMockResponse(string symptoms)
        {
            Console.WriteLine($"Using mock response for symptoms: {symptoms}");

            var mockResponses = new Dictionary<string, (string disease, string cause, string cure, string specialty)>
            {
                { "fever", ("Viral Fever", "Viral infection affecting the body's temperature regulation", "Rest, plenty of fluids, paracetamol for fever reduction", "General Medicine") },
                { "cough", ("Respiratory Infection", "Viral/Bacterial", "Rest + warm fluids", "Pulmonology") },
                { "headache", ("Tension Headache", "Stress / dehydration", "Rest + hydration", "Neurology") },
                { "stomach", ("Gastritis", "Stomach lining inflammation", "Antacids + bland diet", "Gastroenterology") },
                { "chest", ("Chest Congestion", "Respiratory mucus buildup", "Steam + expectorants", "Pulmonology") }
            };

            var match = mockResponses.FirstOrDefault(x => symptoms.ToLower().Contains(x.Key));

            var data = match.Key != null
                ? match.Value
                : ("Common Cold", "Viral infection", "Rest + fluids", "General Medicine");

            return new SymptomAnalysisResponseDto
            {
                PredictedDisease = data.Item1,
                Cause = data.Item2,
                Cure = data.Item3,
                RecommendedSpecialty = data.Item4,
                RecommendedDoctors = new List<DoctorDto>()
            };

        }

        // ✅ Parse actual Gemini JSON
        private SymptomAnalysisResponseDto ParseGeminiResponse(string responseContent)
        {
            Console.WriteLine($"Parsing Gemini Response: {responseContent}");

            try
            {
                var json = JsonDocument.Parse(responseContent);

                var text = json.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                if (string.IsNullOrEmpty(text))
                    return GetMockResponse("unknown");

                // Extract JSON part
                int start = text.IndexOf('{');
                int end = text.LastIndexOf('}');
                if (start == -1 || end == -1 || end <= start)
                    return ExtractFromPlainText(text);

                var jsonText = text.Substring(start, end - start + 1);

                var parsed = JsonDocument.Parse(jsonText);

                return new SymptomAnalysisResponseDto
                {
                    PredictedDisease = parsed.RootElement.GetProperty("predicted_Disease").GetString() ?? "Unknown Disease",
                    Cause = parsed.RootElement.GetProperty("cause").GetString() ?? "Unknown Cause",
                    Cure = parsed.RootElement.GetProperty("cure").GetString() ?? "Consult a doctor",
                    RecommendedSpecialty = parsed.RootElement.GetProperty("recommended_specialty").GetString() ?? "General Medicine",
                    RecommendedDoctors = new List<DoctorDto>()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gemini parsing failed: {ex.Message}");
                return ExtractFromPlainText(responseContent);
            }
        }

        // ✅ Fallback text extractor
        private SymptomAnalysisResponseDto ExtractFromPlainText(string text)
        {
            var lines = text.Split('\n');

            string disease = "General Condition";
            string cause = "Multiple possibilities";
            string cure = "Consult a doctor";

            foreach (var line in lines)
            {
                var lower = line.ToLower();

                if (lower.Contains("disease") || lower.Contains("condition"))
                    disease = line.Trim();

                else if (lower.Contains("cause"))
                    cause = line.Trim();

                else if (lower.Contains("treatment") || lower.Contains("cure"))
                    cure = line.Trim();
            }

            return new SymptomAnalysisResponseDto
            {
                PredictedDisease = disease,
                Cause = cause,
                Cure = cure,
                RecommendedSpecialty = "General Medicine",
                RecommendedDoctors = new List<DoctorDto>()
            };
        }
    }
}
