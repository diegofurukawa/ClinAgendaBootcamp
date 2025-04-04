using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Specialty;

namespace ClinAgendaBootcamp.src.Core.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<(int total, IEnumerable<SpecialtyDTO> specialtys)> GetAllAsync(string? name, int? itemsPerPage, int? page);
        Task<int> InsertSpecialtyAsync(SpecialtyInsertDTO specialtyInsertDTO);
        Task<SpecialtyDTO> GetByIdAsync(int id);
        Task<int> DeleteSpecialtyAsync(int id);
        Task<IEnumerable<SpecialtyDTO>> GetSpecialtiesByIds(List<int> specialtiesId);
    }
}