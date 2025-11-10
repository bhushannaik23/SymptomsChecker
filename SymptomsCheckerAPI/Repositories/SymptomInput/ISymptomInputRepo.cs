using Symptoms_Checker.Models;

namespace Symptoms_Checker.Repositories
{
    public interface ISymptomInputRepository
    {
        Task<SymptomInput> CreateAsync(SymptomInput symptomInput);
    }
}
