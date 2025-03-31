using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Specialty;

namespace ClinAgendaBootcamp.src.Application.DTOs.Appointment
{
    public class AppointmentListReturnDTO
    {
         public int Id { get; set; }
        public required AppointmentPatientReturnDTO Patient { get; set; }
        public required AppointmentDoctorReturnDTO Doctor { get; set; }
        public required SpecialtyDTO Specialty { get; set; }
        public required string AppointmentDate { get; set; }
    }

}