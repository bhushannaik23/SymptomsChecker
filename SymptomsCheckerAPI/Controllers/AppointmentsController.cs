using Microsoft.AspNetCore.Mvc;
using Symptoms_Checker.Services;
using Symptoms_Checker.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symptoms_Checker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // Get all appointments of a patient
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetPatientAppointments(int patientId)
        {
            var appointments = await _appointmentService.GetPatientAppointmentsAsync(patientId);
            return Ok(appointments);
        }

        // Get all appointments of a doctor
        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetDoctorAppointments(int doctorId)
        {
            var appointments = await _appointmentService.GetDoctorAppointmentsAsync(doctorId);
            return Ok(appointments);
        }

        // Create a new appointment
        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment(CreateAppointmentDto createAppointmentDto)
        {
            var appointment = await _appointmentService.CreateAppointmentAsync(createAppointmentDto);
            return Ok(appointment);
        }

        // Update appointment status
        [HttpPut("{appointmentId}/status")]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointmentStatus(int appointmentId, UpdateAppointmentStatusDto updateDto)
        {
            var appointment = await _appointmentService.UpdateAppointmentStatusAsync(appointmentId, updateDto);
            if (appointment == null)
                return NotFound("Appointment not found");

            return Ok(appointment);
        }
    }
}
