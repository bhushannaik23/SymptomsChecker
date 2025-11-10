using Microsoft.AspNetCore.Mvc;
using Symptoms_Checker.DTOs;
using Symptoms_Checker.Services;
using System.Threading.Tasks;

namespace Symptoms_Checker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SymptomAnalysisController : ControllerBase
    {
        private readonly ISymptomAnalysisService _symptomAnalysisService;

        public SymptomAnalysisController(ISymptomAnalysisService symptomAnalysisService)
        {
            _symptomAnalysisService = symptomAnalysisService;
        }

        // Analyze patient symptoms
        [HttpPost("analyze")]
        public async Task<ActionResult<SymptomAnalysisResponseDto>> AnalyzeSymptoms(SymptomAnalysisRequestDto request)
        {
            var result = await _symptomAnalysisService.AnalyzeSymptomsAsync(request);
            return Ok(result);
        }
    }
}
