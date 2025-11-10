using Symptoms_Checker.Models;

namespace Symptoms_Checker.Repositories
{
    public interface IPatientHistoryRepository
    {
        Task<IEnumerable<PatientHistory>> GetByPatientIdAsync(int patientId);
        Task<PatientHistory> CreateAsync(PatientHistory patientHistory);
    }
}
