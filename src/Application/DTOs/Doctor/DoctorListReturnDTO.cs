using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Specialty;
using ClinAgendaBootcamp.src.Application.DTOs.Status;

namespace ClinAgendaBootcamp.src.Application.DTOs.Doctor
{
    public class DoctorListReturnDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required List<SpecialtyDTO> Specialty { get; set; }
        public required StatusDTO Status { get; set; }
    }
}