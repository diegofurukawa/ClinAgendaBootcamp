using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;

namespace ClinAgendaBootcamp.src.Core.Interfaces
{
    public interface IPatientRepository
    {
        Task<PatientDTO> GetPatientByIdAsync(int id);
        Task<int> DeletePatientAsync(int id);
        Task<int> InsertPatientAsync(PatientInsertDTO patientInsertDTO);
        Task<(int total, IEnumerable<PatientDTO> patients)> GetAllAsync(int? itemsPerPage, int? page);
    }
}