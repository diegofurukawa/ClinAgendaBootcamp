using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.Application.DTOs.Appointment
{
    public class AppointmentPatientReturnDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string DocumentNumber { get; set; }
    }
}