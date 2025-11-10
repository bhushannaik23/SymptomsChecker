using Symptoms_Checker.DTOs;

namespace Symptoms_Checker.Services
{
    public interface IGeminiService
    {
        Task<SymptomAnalysisResponseDto> AnalyzeSymptomsAsync(string symptoms);
    }
}
