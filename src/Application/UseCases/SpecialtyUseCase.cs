using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Specialty;
using ClinAgendaBootcamp.src.Core.Interfaces;

namespace ClinAgendaBootcamp.src.Application.SpecialtyUseCase
{
    public class SpecialtyUseCase
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyUseCase(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<object> GetSpecialtyAsync(int itemsPerPage, int page)
        {
            var (total, rawData) = await _specialtyRepository.GetAllSpecialtyAsync(itemsPerPage, page);

            return new
            {
                total,
                items = rawData.ToList()
            };
        }

        public async Task<int> CreateSpecialtyAsync(SpecialtyInsertDTO specialtyDTO)
        {
           
            var newSpecialtyId = await _specialtyRepository.InsertSpecialtyAsync(specialtyDTO);

            return newSpecialtyId;

        }
        public async Task<SpecialtyDTO?> GetSpecialtyByIdAsync(int id)
        {
            return await _specialtyRepository.GetSpecialtyByIdAsync(id);
        }

    }
}