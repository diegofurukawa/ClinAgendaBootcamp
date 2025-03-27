using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.Application.DTOs.Doctor
{
    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public required string Name { get; set; }
        public required int StatusId { get; set; }
    }
}