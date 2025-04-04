using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.Application.DTOs.Patient
{
    public class PatientResponseDTO
    {
        public int Total { get; set; }
        public List<PatientListReturnDTO> Items { get; set; }
    }
}