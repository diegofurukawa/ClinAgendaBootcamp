using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Doctor;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;
using ClinAgendaBootcamp.src.Application.DTOs.Specialty;

namespace ClinAgendaBootcamp.src.Application.DTOs.Appointment
{
    public class AppointmentListReturnDTO
    {
         public int Id { get; set; }
        public required PatientReturnAppointmentDTO Patient { get; set; }
        public required DoctorReturnAppointmentDTO Doctor { get; set; }
        public required SpecialtyDTO Specialty { get; set; }
        public required string AppointmentDate { get; set; }
    }
}