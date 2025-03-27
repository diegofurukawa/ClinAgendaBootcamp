using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Doctor;
using ClinAgendaBootcamp.src.Application.DTOs.Specialty;
using ClinAgendaBootcamp.src.Application.DTOs.Status;
using ClinAgendaBootcamp.src.Core.Interfaces;

namespace ClinAgendaBootcamp.src.Application.DoctorUseCase
{
    public class DoctorUseCase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorSpecialtyRepository _doctorSpecialtyRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        public DoctorUseCase(IDoctorRepository doctorRepository, IDoctorSpecialtyRepository doctorspecialtyRepository, ISpecialtyRepository specialtyRepository)
        {
            _doctorRepository = doctorRepository;
            _doctorSpecialtyRepository = doctorspecialtyRepository;
            _specialtyRepository = specialtyRepository;
        }

        public async Task<object> GetDoctorsAsync(string? name, int? specialtyId, int? statusId, int itemsPerPage, int page)
        {
            int offset = (page - 1) * itemsPerPage;

            var doctors = (await _doctorRepository.GetDoctorsAsync(name, specialtyId, statusId, offset, itemsPerPage)).ToList();

            if (!doctors.Any())
                return new { total = 0, items = new List<DoctorListReturnDTO>() };

            var doctorIds = doctors.Select(d => d.Id).ToArray();
            var specialties = (await _doctorRepository.GetDoctorSpecialtyAsync(doctorIds)).ToList();

            var result = doctors.Select(d => new DoctorListReturnDTO
            {
                Id = d.Id,
                Name = d.Name,
                Status = new StatusDTO
                {
                    Id = d.StatusId,
                    Name = d.StatusName
                },
                Specialty = specialties
            .Where(s => s.DoctorId == d.Id)
            .Select(s => new SpecialtyDTO
            {
                Id = s.SpecialtyId,
                Name = s.SpecialtyName,
                ScheduleDuration = s.ScheduleDuration
            })
            .ToList()
            });

            return new
            {
                total = result.Count(),
                items = result.ToList()
            };
        }
        public async Task<int> CreateDoctorAsync(DoctorInsertDTO doctorDto)
        {
            var newDoctorId = await _doctorRepository.InsertDoctorAsync(doctorDto);

            var doctor_specialities = new DoctorSpecialtyDTO
            {
                DoctorId = newDoctorId,
                SpecialtyId = doctorDto.Specialty
            };

            await _doctorSpecialtyRepository.InsertAsync(doctor_specialities);

            return newDoctorId;
        }


        public async Task<object> GetDoctorByIdAsync(int id)
        {
            var rawData = await _doctorRepository.GetDoctorByIdAsync(id);

            var inforDoctor = new
            {
                item = rawData
                    .GroupBy(item => item.Id)
                    .Select(group => new
                    {
                        id = group.Key,
                        name = group.First().Name,
                        specialty = group
                            .Select(s => new
                            {
                                id = s.SpecialtyId,
                                name = s.SpecialtyName
                            })
                            .ToList(),
                        status = new
                        {
                            id = group.First().StatusId,
                            name = group.First().StatusName
                        }
                    }).First()
                
            };

            return inforDoctor;

        }
        public async Task<bool> UpdateDoctorAsync(int doctorId, DoctorInsertDTO doctorDto)
        {
            var doctorToUpdate = new DoctorDTO
            {
                DoctorId = doctorId,
                Name = doctorDto.Name,
                StatusId = doctorDto.StatusId
            };

            await _doctorRepository.UpdateDoctorAsync(doctorToUpdate);

            await _doctorSpecialtyRepository.DeleteByDoctorIdAsync(doctorId);

            var doctorSpecialties = new DoctorSpecialtyDTO
            {
                DoctorId = doctorId,
                SpecialtyId = doctorDto.Specialty
            };

            await _doctorSpecialtyRepository.InsertAsync(doctorSpecialties);

            return true;
        }
    }

}