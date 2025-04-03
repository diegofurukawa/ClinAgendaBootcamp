using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Doctor;

namespace ClinAgendaBootcamp.src.Core.Interfaces
{
    public interface IDoctorSpecialtyRepository
    {
        Task InsertAsync(DoctorSpecialtyDTO doctor);
        Task DeleteByDoctorIdAsync(int doctorId);
    }
}