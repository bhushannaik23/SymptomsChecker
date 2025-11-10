using Microsoft.AspNetCore.Mvc;
using Symptoms_Checker.DTOs;
using Symptoms_Checker.Services;
using System.Threading.Tasks;

namespace Symptoms_Checker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Login endpoint
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }

        // Register a patient
        [HttpPost("register/patient")]
        public async Task<ActionResult<AuthResponseDto>> RegisterPatient(RegisterPatientDto registerDto)
        {
            var result = await _authService.RegisterPatientAsync(registerDto);
            if (result == null)
                return BadRequest("Email already exists");

            return Ok(result);
        }

        // Register a doctor
        [HttpPost("register/doctor")]
        public async Task<ActionResult<AuthResponseDto>> RegisterDoctor(RegisterDoctorDto registerDto)
        {
            var result = await _authService.RegisterDoctorAsync(registerDto);
            if (result == null)
                return BadRequest("Email already exists");

            return Ok(result);
        }

        // Register an admin
        [HttpPost("register/admin")]
        public async Task<ActionResult<AuthResponseDto>> RegisterAdmin(RegisterAdminDto registerDto)
        {
            var result = await _authService.RegisterAdminAsync(registerDto);
            if (result == null)
                return BadRequest("Email already exists");

            return Ok(result);
        }
    }
}
