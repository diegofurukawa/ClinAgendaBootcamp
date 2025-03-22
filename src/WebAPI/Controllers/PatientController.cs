using ClinAgendaBootcamp.src.Application.DTOs.Patient;
using ClinAgendaBootcamp.src.Application.PatientUseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClinAgendaBootcamp.src.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Patient")]
    public class PatientController : ControllerBase
    {
        private readonly PatientUseCase _patientUseCase;

        public PatientController(PatientUseCase patientUseCase)
        {
            _patientUseCase = patientUseCase;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPatientsAsync([FromQuery] string? name, [FromQuery] string? documentNumber, [FromQuery] int? statusId, [FromQuery] int itemsPerPage = 10, [FromQuery] int page = 1)
        {
            try
            {
                // Validação dos parâmetros de paginação
                if (itemsPerPage <= 0)
                {
                    itemsPerPage = 10;
                }
                
                if (page <= 0)
                {
                    page = 1;
                }
                
                var patients = await _patientUseCase.GetPatientsAsync(name, documentNumber, statusId, itemsPerPage, page);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                // Log detalhado do erro para diagnóstico
                Console.WriteLine($"Erro ao buscar pacientes: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                return StatusCode(500, $"Erro ao buscar pacientes: {ex.Message}");
            }
        }

        [HttpGet("listById/{id}")]
        public async Task<IActionResult> GetPatientByIdAsync(int id)
        {
            try
            {
                var patient = await _patientUseCase.GetPatientDetailsAsync(id);

                if (patient == null)
                {
                    return NotFound($"Paciente com ID {id} não encontrado.");
                }

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar paciente por ID: {ex.Message}");
            }
        }

        // Revised code for the CreatePatientAsync method in PatientController.cs
        [HttpPost("insert")]
        public async Task<IActionResult> CreatePatientAsync([FromBody] PatientInsertDTO patient)
        {
            try
            {
                // Validação básica dos dados
                if (patient == null || string.IsNullOrWhiteSpace(patient.Name))
                {
                    return BadRequest("Os dados do paciente são inválidos.");
                }
                
                // Improved date validation and formatting
                if (!DateTime.TryParse(patient.BirthDate, out DateTime birthDate))
                {
                    return BadRequest("A data de nascimento deve estar em um formato válido (ex: YYYY-MM-DD).");
                }
                
                // Format the date in MySQL accepted format
                patient.BirthDate = birthDate.ToString("yyyy-MM-dd");
                
                var createdPatientId = await _patientUseCase.CreatePatientAsync(patient);
                
                // Retrieve complete patient details
                var createdPatient = await _patientUseCase.GetPatientDetailsAsync(createdPatientId);
                
                if (createdPatient == null)
                {
                    var basicPatient = await _patientUseCase.GetPatientByIdAsync(createdPatientId);
                    return Created($"/api/Patient/listById/{createdPatientId}", basicPatient);
                }

                return Created($"/api/Patient/listById/{createdPatientId}", createdPatient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar paciente: {ex.Message}");
            }
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePatientAsync(int id, [FromBody] PatientInsertDTO patient)
        {
            try
            {
                if (patient == null || string.IsNullOrWhiteSpace(patient.Name))
                {
                    return BadRequest("Os dados do paciente são inválidos.");
                }
                
                // Validar formato da data
                if (!DateTime.TryParse(patient.BirthDate, out DateTime birthDate))
                {
                    return BadRequest("A data de nascimento deve estar no formato YYYY-MM-DD.");
                }
                
                // Formatar a data no formato aceito pelo MySQL
                patient.BirthDate = birthDate.ToString("yyyy-MM-dd");
                
                var success = await _patientUseCase.UpdatePatientAsync(id, patient);
                
                if (!success)
                {
                    return NotFound($"Paciente com ID {id} não encontrado ou não foi possível atualizar.");
                }
                
                var updatedPatient = await _patientUseCase.GetPatientDetailsAsync(id);
                return Ok(updatedPatient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar paciente: {ex.Message}");
            }
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePatientAsync(int id)
        {
            try
            {
                var success = await _patientUseCase.DeletePatientAsync(id);
                
                if (!success)
                {
                    return NotFound($"Paciente com ID {id} não encontrado ou não foi possível excluir.");
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir paciente: {ex.Message}");
            }
        }
    }
}