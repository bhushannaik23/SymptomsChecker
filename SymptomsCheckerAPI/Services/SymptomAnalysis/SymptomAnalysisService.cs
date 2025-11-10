using Symptoms_Checker.DTOs;
using Symptoms_Checker.Models;
using Symptoms_Checker.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Symptoms_Checker.Services
{
    public class SymptomAnalysisService : ISymptomAnalysisService
    {
        private readonly IGeminiService _geminiService;
        private readonly IPatientHistoryRepository _patientHistoryRepository;
        private readonly ISymptomInputRepository _symptomInputRepository;
        private readonly IDoctorRepository _doctorRepository;

        public SymptomAnalysisService(
            IGeminiService geminiService,
            IPatientHistoryRepository patientHistoryRepository,
            ISymptomInputRepository symptomInputRepository,
            IDoctorRepository doctorRepository)
        {
            _geminiService = geminiService;
            _patientHistoryRepository = patientHistoryRepository;
            _symptomInputRepository = symptomInputRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<SymptomAnalysisResponseDto> AnalyzeSymptomsAsync(SymptomAnalysisRequestDto request)
        {
            // 1. Save symptom input
            await _symptomInputRepository.CreateAsync(new SymptomInput
            {
                PatientId = request.PatientId,
                SymptomsText = request.SymptomsText
            });

            // 2. Get analysis from Gemini
            var analysis = await _geminiService.AnalyzeSymptomsAsync(request.SymptomsText);

            // 3. Save to patient history
            await _patientHistoryRepository.CreateAsync(new PatientHistory
            {
                PatientId = request.PatientId,
                SymptomsText = request.SymptomsText,
                DiseaseSuggested = analysis.PredictedDisease
            });

            // 4. Get recommended doctors based on specialty
            var doctors = await _doctorRepository.GetDoctorsBySpecialtyAsync(analysis.RecommendedSpecialty);

            // 5. Fallback to General Medicine if no doctors found
            if (doctors == null || !doctors.Any())
            {
                doctors = await _doctorRepository.GetDoctorsBySpecialtyAsync("General Medicine");
            }

            // 6. Map doctors to DTO
            analysis.RecommendedDoctors = doctors.Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone
            }).ToList();

            return analysis;
        }
    }
}
