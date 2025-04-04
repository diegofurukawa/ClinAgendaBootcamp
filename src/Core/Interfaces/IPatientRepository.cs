using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;

namespace ClinAgendaBootcamp.src.Core.Interfaces
{
    public interface IPatientRepository
    {
        Task<(int total, IEnumerable<PatientListDTO> patient)> GetPatientsAsync(string? name, string? documentNumber, int? statusId, int itemsPerPage, int page);
        Task<int> InsertPatientAsync(PatientInsertDTO patient);
        Task<PatientDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(PatientDTO patient);
        Task<int> DeleteByPatientIdAsync(int patientId);
        Task<IEnumerable<PatientListDTO>> AutoComplete(string name);
    }
}