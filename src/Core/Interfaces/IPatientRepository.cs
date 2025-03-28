using System.Collections.Generic;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;

namespace ClinAgendaBootcamp.src.Core.Interfaces
{
    public interface IPatientRepository
    {        
        Task<PatientDTO> GetPatientByIdAsync(int id);      
        Task<(int total, IEnumerable<PatientListDTO> patient)> GetPatientsAsync(string? name, string? documentNumber, int? statusId, int itemsPerPage, int page);          
        Task<(int total, IEnumerable<PatientListDTO> patient)> GetPatientDetailsAsync(string? name, string? documentNumber, int? statusId, int itemsPerPage, int page);
        Task<int> DeletePatientAsync(int id);
        Task<int> InsertPatientAsync(PatientInsertDTO patientInsertDTO);
        Task<int> UpdatePatientAsync(int id, PatientInsertDTO patientInsertDTO);
    }
}