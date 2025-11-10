using Microsoft.EntityFrameworkCore;
using Symptoms_Checker.Data;
using Symptoms_Checker.Models;

namespace Symptoms_Checker.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task<Doctor> UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsBySpecialtyAsync(string specialty)
        {
            return await _context.Doctors
                .Include(d => d.DoctorSpecialties)
                    .ThenInclude(ds => ds.Specialty)
                .Where(d => d.DoctorSpecialties
                    .Any(ds => ds.Specialty.Name == specialty))
                .ToListAsync();
        }
    }
}
