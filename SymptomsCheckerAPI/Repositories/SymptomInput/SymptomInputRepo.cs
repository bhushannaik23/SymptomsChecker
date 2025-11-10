using Symptoms_Checker.Data;
using Symptoms_Checker.Models;

namespace Symptoms_Checker.Repositories
{
    public class SymptomInputRepository : ISymptomInputRepository
    {
        private readonly ApplicationDbContext _context;

        public SymptomInputRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SymptomInput> CreateAsync(SymptomInput symptomInput)
        {
            _context.SymptomInputs.Add(symptomInput);
            await _context.SaveChangesAsync();
            return symptomInput;
        }
    }
}
