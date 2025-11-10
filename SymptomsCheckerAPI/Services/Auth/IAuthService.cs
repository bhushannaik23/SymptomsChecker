using Symptoms_Checker.DTOs;

namespace Symptoms_Checker.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto?> RegisterPatientAsync(RegisterPatientDto registerDto);
        Task<AuthResponseDto?> RegisterDoctorAsync(RegisterDoctorDto registerDto);
        Task<AuthResponseDto?> RegisterAdminAsync(RegisterAdminDto registerDto);
    }
}
