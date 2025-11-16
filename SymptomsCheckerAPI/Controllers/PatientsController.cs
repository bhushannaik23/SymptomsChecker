using Microsoft.AspNetCore.Mvc;
using Symptoms_Checker.DTOs;
using Symptoms_Checker.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symptoms_Checker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public PatientsController(IPatientService patientService, IAppointmentService appointmentService)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
        }

        // Get all patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        // Get a patient by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatient(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        // Create a new patient
        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatient(CreatePatientDto createPatientDto)
        {
            var patient = await _patientService.CreatePatientAsync(createPatientDto);
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }

        // Update patient details
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> UpdatePatient(int id, UpdatePatientDto updatePatientDto)
        {
            var patient = await _patientService.UpdatePatientAsync(id, updatePatientDto);
            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        // Get all appointments of a patient
        [HttpGet("{id}/appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetPatientAppointments(int id)
        {
            var appointments = await _appointmentService.GetPatientAppointmentsAsync(id);
            return Ok(appointments);
        }
    }
}
