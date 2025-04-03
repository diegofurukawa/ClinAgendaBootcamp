using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Appointment;
using ClinAgendaBootcamp.src.Application.DTOs.Doctor;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;
using ClinAgendaBootcamp.src.Application.DTOs.Specialty;
using ClinAgendaBootcamp.src.Core.Interfaces;

namespace ClinAgendaBootcamp.src.Application.AppointmentUseCase
{
    public class AppointmentUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialtyRepository _specialtyRepository;

        public AppointmentUseCase(
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository,
            ISpecialtyRepository specialtyRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _specialtyRepository = specialtyRepository;
        }

        public async Task<object> GetAppointmentsAsync(
            string? patientName, 
            string? doctorName, 
            int? specialtyId, 
            int itemsPerPage, 
            int page
            )
        {
            var (total, appointments) = await _appointmentRepository.GetAppointmentsAsync(
                patientName, 
                doctorName, 
                specialtyId, 
                itemsPerPage, 
                page
            );

            var appointmentsList = appointments.Select(a => new AppointmentListReturnDTO
            {
                Id = a.Id,
                Patient = new AppointmentPatientReturnDTO
                {
                    Name = a.PatientName,
                    DocumentNumber = a.PatientDocument
                },
                Doctor = new AppointmentDoctorReturnDTO
                {
                    Name = a.DoctorName
                },
                Specialty = new SpecialtyDTO
                {
                    Id = a.SpecialtyId,
                    Name = a.SpecialtyName,
                    ScheduleDuration = a.ScheduleDuration
                },
                AppointmentDate = a.AppointmentDate
            }).ToList();

            return new
            {
                total,
                items = appointmentsList
            };
        }

        public async Task<int> CreateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            // Valida que Paciente exista
            var patient = await _patientRepository.GetPatientByIdAsync(appointmentDTO.PatientId);
            if (patient == null)
            {
                throw new ArgumentException($"Patient with ID {appointmentDTO.PatientId} does not exist.");
            }

            // Valida que o doutor exista e tenha especialidade
            var doctorData = (await _doctorRepository.GetDoctorByIdAsync(appointmentDTO.DoctorId)).ToList();
            if (!doctorData.Any())
            {
                throw new ArgumentException($"Doctor with ID {appointmentDTO.DoctorId} does not exist.");
            }

            // check da especialidade do Doutor
            var doctorSpecialties = doctorData.Select(d => d.SpecialtyId).Distinct().ToList();
            if (!doctorSpecialties.Contains(appointmentDTO.SpecialtyId))
            {
                throw new ArgumentException($"Doctor with ID {appointmentDTO.DoctorId} does not have the specialty with ID {appointmentDTO.SpecialtyId}.");
            }

            // Valida se especialidade existe
            var specialty = await _specialtyRepository.GetSpecialtyByIdAsync(appointmentDTO.SpecialtyId);
            if (specialty == null)
            {
                throw new ArgumentException($"Specialty with ID {appointmentDTO.SpecialtyId} does not exist.");
            }

            // Valida formato de data do appointment
            if (!DateTime.TryParse(appointmentDTO.AppointmentDate, out DateTime appointmentDate))
            {
                throw new ArgumentException("The appointment date must be in a valid format (YYYY-MM-DD HH:MM:SS).");
            }

            // Insera appointment
            return await _appointmentRepository.InsertAppointmentAsync(appointmentDTO);
        }

        public async Task<AppointmentDTO?> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }

        public async Task<bool> UpdateAppointmentAsync(int id, AppointmentInsertDTO appointmentDTO)
        {
            // Validate if appointment exists
            var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (existingAppointment == null)
            {
                return false;
            }

            // Validate if Patient exists
            var patient = await _patientRepository.GetPatientByIdAsync(appointmentDTO.PatientId);
            if (patient == null)
            {
                throw new ArgumentException($"Patient with ID {appointmentDTO.PatientId} does not exist.");
            }

            // Validate if Doctor exists and has the Specialty
            var doctorData = (await _doctorRepository.GetDoctorByIdAsync(appointmentDTO.DoctorId)).ToList();
            if (!doctorData.Any())
            {
                throw new ArgumentException($"Doctor with ID {appointmentDTO.DoctorId} does not exist.");
            }

            // Check if doctor has the specified specialty
            var doctorSpecialties = doctorData.Select(d => d.SpecialtyId).Distinct().ToList();
            if (!doctorSpecialties.Contains(appointmentDTO.SpecialtyId))
            {
                throw new ArgumentException($"Doctor with ID {appointmentDTO.DoctorId} does not have the specialty with ID {appointmentDTO.SpecialtyId}.");
            }

            // Validate if Specialty exists
            var specialty = await _specialtyRepository.GetSpecialtyByIdAsync(appointmentDTO.SpecialtyId);
            if (specialty == null)
            {
                throw new ArgumentException($"Specialty with ID {appointmentDTO.SpecialtyId} does not exist.");
            }

            // Validate appointment date format
            if (!DateTime.TryParse(appointmentDTO.AppointmentDate, out DateTime appointmentDate))
            {
                throw new ArgumentException("The appointment date must be in a valid format (YYYY-MM-DD HH:MM:SS).");
            }

            // Update appointment
            appointmentDTO.Id = id;
            return await _appointmentRepository.UpdateAppointmentAsync(appointmentDTO);
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            // Check if appointment exists
            var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (existingAppointment == null)
            {
                return false;
            }

            // Delete appointment
            var rowsAffected = await _appointmentRepository.DeleteAppointmentAsync(id);
            return rowsAffected > 0;
        }
    }
}