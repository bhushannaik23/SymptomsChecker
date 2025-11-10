using Symptoms_Checker.Models;

namespace Symptoms_Checker.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);
        Task<Appointment> CreateAsync(Appointment appointment);
        Task<Appointment> UpdateAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(int id);
    }
}
