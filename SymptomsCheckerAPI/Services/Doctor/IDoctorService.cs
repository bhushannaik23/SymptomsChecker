using Symptoms_Checker.DTOs;

namespace Symptoms_Checker.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto?> GetDoctorByIdAsync(int id);
        Task<bool> DeleteDoctorAsync(int id);
        Task<DoctorDto?> UpdateDoctorAsync(int id, UpdateDoctorDto updateDoctorDto);
    }
}
