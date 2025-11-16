using Symptoms_Checker.DTOs;
using Symptoms_Checker.Models;
using Symptoms_Checker.Repositories;

namespace Symptoms_Checker.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<AppointmentDto>> GetPatientAppointmentsAsync(int patientId)
        {
            var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);

            return appointments.Select(a => new AppointmentDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                PatientName = a.Patient?.Name ?? "Unknown Patient",
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor?.Name ?? "Unknown Doctor",
                DateTime = a.DateTime,
                Status = a.Status
            });
        }

        public async Task<IEnumerable<AppointmentDto>> GetDoctorAppointmentsAsync(int doctorId)
        {
            var appointments = await _appointmentRepository.GetByDoctorIdAsync(doctorId);

            return appointments.Select(a => new AppointmentDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                PatientName = a.Patient.Name,
                DoctorId = a.DoctorId,
                DateTime = a.DateTime,
                Status = a.Status
            });
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
        {
            var appointment = new Appointment
            {
                PatientId = createAppointmentDto.PatientId,
                DoctorId = createAppointmentDto.DoctorId,
                DateTime = createAppointmentDto.DateTime,
                Status = "Scheduled"
            };

            var createdAppointment = await _appointmentRepository.CreateAsync(appointment);
            
            // Fetch the appointment with related entities to get names
            var appointmentWithDetails = await _appointmentRepository.GetByIdAsync(createdAppointment.AppointmentId);
            
            return new AppointmentDto
            {
                AppointmentId = createdAppointment.AppointmentId,
                PatientId = createdAppointment.PatientId,
                PatientName = appointmentWithDetails?.Patient?.Name ?? "Unknown Patient",
                DoctorId = createdAppointment.DoctorId,
                DoctorName = appointmentWithDetails?.Doctor?.Name ?? "Unknown Doctor",
                DateTime = createdAppointment.DateTime,
                Status = createdAppointment.Status
            };
        }

        public async Task<AppointmentDto?> UpdateAppointmentStatusAsync(int appointmentId, UpdateAppointmentStatusDto updateDto)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment == null)
                return null;

            appointment.Status = updateDto.Status;

            var updatedAppointment = await _appointmentRepository.UpdateAsync(appointment);

            return new AppointmentDto
            {
                AppointmentId = updatedAppointment.AppointmentId,
                PatientId = updatedAppointment.PatientId,
                DoctorId = updatedAppointment.DoctorId,
                DateTime = updatedAppointment.DateTime,
                Status = updatedAppointment.Status
            };
        }
    }
}
