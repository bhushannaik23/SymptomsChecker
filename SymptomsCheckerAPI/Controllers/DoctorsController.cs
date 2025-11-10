using Microsoft.AspNetCore.Mvc;
using Symptoms_Checker.DTOs;
using Symptoms_Checker.Repositories;
using Symptoms_Checker.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Symptoms_Checker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorRepository doctorRepository, IDoctorService doctorService)
        {
            _doctorRepository = doctorRepository;
            _doctorService = doctorService;
        }

        // Get all doctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            var doctorDtos = doctors.Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone,
                DateCreated = d.DateCreated
            }).ToList();

            return Ok(doctorDtos);
        }

        // Get a doctor by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound("Doctor not found");

            return Ok(doctor);
        }

        // Update a doctor
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDto>> UpdateDoctor(int id, UpdateDoctorDto updateDoctorDto)
        {
            var doctor = await _doctorService.UpdateDoctorAsync(id, updateDoctorDto);
            if (doctor == null)
                return NotFound("Doctor not found");

            return Ok(doctor);
        }

        // Get doctors by specialty
        [HttpGet("specialty/{specialty}")]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctorsBySpecialty(string specialty)
        {
            var doctors = await _doctorRepository.GetDoctorsBySpecialtyAsync(specialty);
            var doctorDtos = doctors.Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone,
                DateCreated = d.DateCreated
            }).ToList();

            return Ok(doctorDtos);
        }
    }
}
