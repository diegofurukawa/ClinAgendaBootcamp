using System.Collections.Generic;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;

namespace ClinAgendaBootcamp.src.Core.Interfaces
{
    public interface IPatientRepository
    {
        Task<PatientDTO> GetPatientByIdAsync(int id);
        Task<PatientListDTO> GetPatientDetailsAsync(int id);
        Task<int> DeletePatientAsync(int id);
        Task<int> InsertPatientAsync(PatientInsertDTO patientInsertDTO);
        Task<int> UpdatePatientAsync(int id, PatientInsertDTO patientInsertDTO);
        Task<(int total, IEnumerable<PatientDTO> patients)> GetAllAsync(int? itemsPerPage, int? page);
        Task<(int total, IEnumerable<PatientListDTO> patients)> GetAllWithDetailsAsync(int? itemsPerPage, int? page);
    }
}