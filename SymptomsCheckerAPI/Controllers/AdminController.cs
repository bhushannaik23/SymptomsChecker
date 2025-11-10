using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Symptoms_Checker.Services;
using Symptoms_Checker.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symptoms_Checker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Uncomment if you want Admin-only access
    // [Authorize(Policy = "AdminOnly")]
    public class AdminController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public AdminController(IPatientService patientService, IDoctorService doctorService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
        }

        [HttpGet("patients")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [HttpDelete("patients/{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (!result)
                return NotFound("Patient not found");

            return Ok("Patient deleted successfully");
        }

        [HttpGet("doctors")]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }
    }
}
