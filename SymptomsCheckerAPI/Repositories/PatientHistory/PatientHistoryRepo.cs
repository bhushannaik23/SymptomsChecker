using Microsoft.EntityFrameworkCore;
using Symptoms_Checker.Data;
using Symptoms_Checker.Models;

namespace Symptoms_Checker.Repositories
{
    public class PatientHistoryRepository : IPatientHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientHistory>> GetByPatientIdAsync(int patientId)
        {
            return await _context.PatientHistories
                .Where(ph => ph.PatientId == patientId)
                .OrderByDescending(ph => ph.DateCreated)
                .ToListAsync();
        }

        public async Task<PatientHistory> CreateAsync(PatientHistory patientHistory)
        {
            _context.PatientHistories.Add(patientHistory);
            await _context.SaveChangesAsync();
            return patientHistory;
        }
    }
}
