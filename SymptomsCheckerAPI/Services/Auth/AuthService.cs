using Microsoft.EntityFrameworkCore;
using Symptoms_Checker.Data;
using Symptoms_Checker.DTOs;
using Symptoms_Checker.Models;
using Symptoms_Checker.Repositories;

namespace Symptoms_Checker.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(ApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            switch (loginDto.UserType.ToLower())
            {
                case "patient":
                    var patient = await _context.Patients
                        .FirstOrDefaultAsync(p => p.Email == loginDto.Email && p.Password == loginDto.Password);

                    if (patient == null) return null;

                    return new AuthResponseDto
                    {
                        Token = _jwtService.GenerateToken(patient.PatientId, patient.Email, "patient"),
                        UserType = "patient",
                        UserId = patient.PatientId,
                        Name = patient.Name,
                        Email = patient.Email
                    };

                case "doctor":
                    var doctor = await _context.Doctors
                        .FirstOrDefaultAsync(d => d.Email == loginDto.Email && d.Password == loginDto.Password);

                    if (doctor == null) return null;

                    return new AuthResponseDto
                    {
                        Token = _jwtService.GenerateToken(doctor.DoctorId, doctor.Email, "doctor"),
                        UserType = "doctor",
                        UserId = doctor.DoctorId,
                        Name = doctor.Name,
                        Email = doctor.Email
                    };

                case "admin":
                    var admin = await _context.Admins
                        .FirstOrDefaultAsync(a => a.Email == loginDto.Email && a.Password == loginDto.Password);

                    if (admin == null) return null;

                    return new AuthResponseDto
                    {
                        Token = _jwtService.GenerateToken(admin.AdminId, admin.Email, "admin"),
                        UserType = "admin",
                        UserId = admin.AdminId,
                        Name = admin.Name,
                        Email = admin.Email
                    };

                default:
                    return null;
            }
        }

        public async Task<AuthResponseDto?> RegisterPatientAsync(RegisterPatientDto registerDto)
        {
            if (await _context.Patients.AnyAsync(p => p.Email == registerDto.Email))
                return null;

            var patient = new Patient
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password,
                Phone = registerDto.Phone
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(patient.PatientId, patient.Email, "patient"),
                UserType = "patient",
                UserId = patient.PatientId,
                Name = patient.Name,
                Email = patient.Email
            };
        }

        public async Task<AuthResponseDto?> RegisterDoctorAsync(RegisterDoctorDto registerDto)
        {
            if (await _context.Doctors.AnyAsync(d => d.Email == registerDto.Email))
                return null;

            var doctor = new Doctor
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password,
                Phone = registerDto.Phone
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            // Add specialties
            foreach (var specialtyId in registerDto.SpecialtyIds)
            {
                _context.DoctorSpecialties.Add(new DoctorSpecialty
                {
                    DoctorId = doctor.DoctorId,
                    SpecialtyId = specialtyId
                });
            }

            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(doctor.DoctorId, doctor.Email, "doctor"),
                UserType = "doctor",
                UserId = doctor.DoctorId,
                Name = doctor.Name,
                Email = doctor.Email
            };
        }

        public async Task<AuthResponseDto?> RegisterAdminAsync(RegisterAdminDto registerDto)
        {
            if (await _context.Admins.AnyAsync(a => a.Email == registerDto.Email))
                return null;

            var admin = new Admin
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(admin.AdminId, admin.Email, "admin"),
                UserType = "admin",
                UserId = admin.AdminId,
                Name = admin.Name,
                Email = admin.Email
            };
        }
    }
}
