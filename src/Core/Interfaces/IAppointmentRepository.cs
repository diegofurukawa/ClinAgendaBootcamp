using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Appointment;

namespace ClinAgendaBootcamp.src.Core.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<(int total, IEnumerable<AppointmentListDTO> appointment)> GetAppointmentsAsync(string? patientName, string? doctorName, int? specialtyId, int itemsPerPage, int page);
        Task<int> InsertAppointmentAsync(AppointmentDTO appointment);
        Task<AppointmentDTO?> GetAppointmentByIdAsync(int id);
        Task<bool> UpdateAppointmentAsync(AppointmentInsertDTO appointment);
        Task<int> DeleteAppointmentAsync(int appointmentId);
    }
}