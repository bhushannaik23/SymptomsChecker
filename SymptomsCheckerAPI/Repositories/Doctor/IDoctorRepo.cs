using Symptoms_Checker.Models;

namespace Symptoms_Checker.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor> UpdateAsync(Doctor doctor);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(string specialty);
    }
}
