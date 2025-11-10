using Symptoms_Checker.DTOs;

namespace Symptoms_Checker.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetPatientAppointmentsAsync(int patientId);
        Task<IEnumerable<AppointmentDto>> GetDoctorAppointmentsAsync(int doctorId);
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task<AppointmentDto?> UpdateAppointmentStatusAsync(int appointmentId, UpdateAppointmentStatusDto updateDto);
    }
}
