using Symptoms_Checker.DTOs;

namespace Symptoms_Checker.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto);
        Task<bool> DeletePatientAsync(int id);
        Task<PatientDto?> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto);
    }
}
