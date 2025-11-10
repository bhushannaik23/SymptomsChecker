using Microsoft.EntityFrameworkCore;
using Symptoms_Checker.Data;
using Symptoms_Checker.DTOs;
using Symptoms_Checker.Models;
using Symptoms_Checker.Repositories;

namespace Symptoms_Checker.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ApplicationDbContext _context;

        public DoctorService(IDoctorRepository doctorRepository, ApplicationDbContext context)
        {
            _doctorRepository = doctorRepository;
            _context = context;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllAsync();

            return doctors.Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                Name = d.Name,
                Email = d.Email,
                Phone = d.Phone,
                DateCreated = d.DateCreated
            });
        }

        public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
                return null;

            return new DoctorDto
            {
                DoctorId = doctor.DoctorId,
                Name = doctor.Name,
                Email = doctor.Email,
                Phone = doctor.Phone,
                DateCreated = doctor.DateCreated
            };
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            return await _doctorRepository.DeleteAsync(id);
        }

        public async Task<DoctorDto?> UpdateDoctorAsync(int id, UpdateDoctorDto updateDoctorDto)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
                return null;

            doctor.Name = updateDoctorDto.Name;
            doctor.Phone = updateDoctorDto.Phone;

            // Update specialties if provided
            if (updateDoctorDto.SpecialtyIds != null && updateDoctorDto.SpecialtyIds.Any())
            {
                // Remove old specialties
                var existingSpecialties = await _context.DoctorSpecialties
                    .Where(ds => ds.DoctorId == id)
                    .ToListAsync();

                _context.DoctorSpecialties.RemoveRange(existingSpecialties);

                // Add new specialties
                foreach (var specialtyId in updateDoctorDto.SpecialtyIds)
                {
                    _context.DoctorSpecialties.Add(new DoctorSpecialty
                    {
                        DoctorId = id,
                        SpecialtyId = specialtyId
                    });
                }

                await _context.SaveChangesAsync();
            }

            var updatedDoctor = await _doctorRepository.UpdateAsync(doctor);

            return new DoctorDto
            {
                DoctorId = updatedDoctor.DoctorId,
                Name = updatedDoctor.Name,
                Email = updatedDoctor.Email,
                Phone = updatedDoctor.Phone,
                DateCreated = updatedDoctor.DateCreated
            };
        }
    }
}
