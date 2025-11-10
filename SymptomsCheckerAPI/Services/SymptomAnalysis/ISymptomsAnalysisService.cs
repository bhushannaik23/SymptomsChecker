using Symptoms_Checker.DTOs;

namespace Symptoms_Checker.Services
{
    public interface ISymptomAnalysisService
    {
        Task<SymptomAnalysisResponseDto> AnalyzeSymptomsAsync(SymptomAnalysisRequestDto request);
    }
}
