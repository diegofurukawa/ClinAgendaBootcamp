using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.Application.DTOs.Specialty
{
    public class SpecialtyDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int ScheduleDuration { get; set; }
    }
}