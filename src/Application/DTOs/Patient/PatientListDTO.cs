using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Status;

namespace ClinAgendaBootcamp.src.Application.DTOs.Patient
{
    public class PatientListDTO
    {
        public int Id { get; set; }
         public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string DocumentNumber { get; set; }
        public required StatusDTO Status { get; set; }
        public required string BirthDate { get; set; }
    }
}