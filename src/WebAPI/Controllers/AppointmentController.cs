using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.AppointmentUseCase;
using ClinAgendaBootcamp.src.Application.DTOs.Appointment;
using ClinAgendaBootcamp.src.Application.PatientUseCase;
using ClinAgendaBootcamp.src.Application.DoctorUseCase;
using ClinAgendaBootcamp.src.Application.SpecialtyUseCase;
using Microsoft.AspNetCore.Mvc;

namespace ClinAgendaBootcamp.src.WebAPI.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentUseCase _appointmentUseCase;
        private readonly PatientUseCase _patientUseCase;
        private readonly DoctorUseCase _doctorUseCase;
        private readonly SpecialtyUseCase _specialtyUseCase;

        public AppointmentController(
            AppointmentUseCase appointmentUseCase, 
            PatientUseCase patientUseCase, 
            DoctorUseCase doctorUseCase, 
            SpecialtyUseCase specialtyUseCase)
        {
            _appointmentUseCase = appointmentUseCase;
            _patientUseCase = patientUseCase;
            _doctorUseCase = doctorUseCase;
            _specialtyUseCase = specialtyUseCase;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAppointmentsAsync(
            [FromQuery] string? patientName, 
            [FromQuery] string? doctorName, 
            [FromQuery] int? specialtyId, 
            [FromQuery] int itemsPerPage = 10, 
            [FromQuery] int page = 1)
        {
            try
            {
                var result = await _appointmentUseCase.GetAppointmentsAsync(
                    patientName, 
                    doctorName, 
                    specialtyId, 
                    itemsPerPage, 
                    page);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("listById/{id}")]
        public async Task<IActionResult> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var appointment = await _appointmentUseCase.GetAppointmentByIdAsync(id);
                
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {id} not found.");
                }
                
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("insert")]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] AppointmentDTO appointment)
        {
            try
            {
                if (appointment == null)
                {
                    return BadRequest("Appointment data is invalid.");
                }

                // Validate if Patient exists
                var patient = await _patientUseCase.GetPatientByIdAsync(appointment.PatientId);
                if (patient == null)
                {
                    return BadRequest($"Patient with ID {appointment.PatientId} does not exist.");
                }

                // Validate if Doctor exists
                var doctor = await _doctorUseCase.GetDoctorByIdAsync(appointment.DoctorId);
                if (doctor == null)
                {
                    return BadRequest($"Doctor with ID {appointment.DoctorId} does not exist.");
                }

                // Validate if Specialty exists
                var specialty = await _specialtyUseCase.GetSpecialtyByIdAsync(appointment.SpecialtyId);
                if (specialty == null)
                {
                    return BadRequest($"Specialty with ID {appointment.SpecialtyId} does not exist.");
                }

                // Create appointment
                var createdAppointmentId = await _appointmentUseCase.CreateAppointmentAsync(appointment);
                
                // Retrieve the created appointment
                var createdAppointment = await _appointmentUseCase.GetAppointmentByIdAsync(createdAppointmentId);
                
                return CreatedAtAction(nameof(GetAppointmentByIdAsync), new { id = createdAppointmentId }, createdAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAppointmentAsync(int id, [FromBody] AppointmentInsertDTO appointment)
        {
            try
            {
                if (appointment == null)
                {
                    return BadRequest("Appointment data is invalid.");
                }

                // Update appointment
                bool updated = await _appointmentUseCase.UpdateAppointmentAsync(id, appointment);
                
                if (!updated)
                {
                    return NotFound($"Appointment with ID {id} not found or could not be updated.");
                }
                
                // Retrieve the updated appointment
                var updatedAppointment = await _appointmentUseCase.GetAppointmentByIdAsync(id);
                
                return Ok(updatedAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAppointmentAsync(int id)
        {
            try
            {
                bool deleted = await _appointmentUseCase.DeleteAppointmentAsync(id);
                
                if (!deleted)
                {
                    return NotFound($"Appointment with ID {id} not found or could not be deleted.");
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}