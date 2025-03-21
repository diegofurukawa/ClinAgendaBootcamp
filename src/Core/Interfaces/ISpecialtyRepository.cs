using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Specialty;
using ClinAgendaBootcamp.src.Core.Entities;

namespace ClinAgendaBootcamp.src.Core.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<SpecialtyDTO> GetByIdAsync(int id);
        Task<int> DeleteSpecialtyAsync(int id);
        Task<int> InsertSpecialtyAsync(SpecialtyInsertDTO SpecialtyInsertDTO);
        Task<(int total, IEnumerable<SpecialtyDTO> specialtys)> GetAllAsync(int? itemsPerPage, int? page);
        Task<int> InsertSpecialtyAsync(Specialty specialty);
    }
}