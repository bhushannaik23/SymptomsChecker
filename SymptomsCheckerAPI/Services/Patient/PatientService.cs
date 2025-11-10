using Symptoms_Checker.DTOs;
using Symptoms_Checker.Models;
using Symptoms_Checker.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Symptoms_Checker.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllAsync();
            return patients.Select(p => new PatientDto
            {
                PatientId = p.PatientId,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                DateCreated = p.DateCreated
            });
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) return null;

            return new PatientDto
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                Email = patient.Email,
                Phone = patient.Phone,
                DateCreated = patient.DateCreated
            };
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto)
        {
            var patient = new Patient
            {
                Name = createPatientDto.Name,
                Email = createPatientDto.Email,
                Password = createPatientDto.Password,
                Phone = createPatientDto.Phone
            };

            var createdPatient = await _patientRepository.CreateAsync(patient);

            return new PatientDto
            {
                PatientId = createdPatient.PatientId,
                Name = createdPatient.Name,
                Email = createdPatient.Email,
                Phone = createdPatient.Phone,
                DateCreated = createdPatient.DateCreated
            };
        }

        public async Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null) return null;

            patient.Name = updatePatientDto.Name;
            patient.Phone = updatePatientDto.Phone;

            var updatedPatient = await _patientRepository.UpdateAsync(patient);

            return new PatientDto
            {
                PatientId = updatedPatient.PatientId,
                Name = updatedPatient.Name,
                Email = updatedPatient.Email,
                Phone = updatedPatient.Phone,
                DateCreated = updatedPatient.DateCreated
            };
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            return await _patientRepository.DeleteAsync(id);
        }
    }
}
