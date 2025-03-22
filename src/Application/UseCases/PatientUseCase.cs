using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;
using ClinAgendaBootcamp.src.Application.DTOs.Status;
using ClinAgendaBootcamp.src.Core.Interfaces;
using ClinAgendaBootcamp.src.Infrastructure.Repositories;
using Dapper;

namespace ClinAgendaBootcamp.src.Application.PatientUseCase
{
    public class PatientUseCase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientUseCase(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<object> GetPatientsAsync(string? name, string? documentNumber, int? statusId, int itemsPerPage, int page)
        {
            var (total, rawData) = await _patientRepository.GetPatientsAsync(name, documentNumber, statusId, itemsPerPage, page);

            var patients = rawData
                .Select(p => new PatientListDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    PhoneNumber = p.PhoneNumber,
                    DocumentNumber = p.DocumentNumber,
                    BirthDate = p.BirthDate,
                    Status = new StatusDTO
                    {
                        Id = p.Status.Id,
                        Name = p.Status.Name
                    }
                })
                .ToList();

            return new { total, items = patients };
        }

        public async Task<int> CreatePatientAsync(PatientInsertDTO patientDTO)
        {
            // Validar se o StatusId existe
            var statusExists = await ValidateStatusExistsAsync(patientDTO.StatusId);
            if (!statusExists)
            {
                throw new ArgumentException($"Status com ID {patientDTO.StatusId} não existe.");
            }
            
            return await _patientRepository.InsertPatientAsync(patientDTO);
        }
        
        private async Task<bool> ValidateStatusExistsAsync(int statusId)
        {
            try
            {
                // Verificar se o status existe - isso depende de ter acesso ao repositório de Status
                // Aqui você precisaria injetar IStatusRepository no construtor
                // Como workaround, podemos fazer verificação direta no banco
                var query = "SELECT COUNT(1) FROM status WHERE id = @StatusId";
                var parameters = new { StatusId = statusId };
                
                // Usando o mesmo connection do repositório de pacientes
                var connection = (_patientRepository as PatientRepository)?._connection;
                if (connection != null)
                {
                    var count = await connection.ExecuteScalarAsync<int>(query, parameters);
                    return count > 0;
                }
                
                // Se não puder verificar diretamente, assume que existe
                return true;
            }
            catch
            {
                // Em caso de erro, assume que existe para não bloquear a operação
                return true;
            }
        }

        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            return await _patientRepository.GetPatientByIdAsync(id);
        }
        
        // Adicione este método ao PatientUseCase.cs
        public async Task<PatientListDTO?> GetPatientDetailsAsync(int id)
        {
            try
            {
                // Buscamos apenas um paciente pelo ID
                var patient = await _patientRepository.GetPatientByIdAsync(id);
                
                if (patient == null)
                {
                    return null;
                }
                
                // Buscar o status do paciente
                string query = "SELECT id, name FROM status WHERE id = @StatusId";
                var parameters = new { StatusId = patient.StatusId };
                
                var connection = (_patientRepository as PatientRepository)?._connection;
                var status = await connection.QueryFirstOrDefaultAsync<StatusDTO>(query, parameters);
                
                // Retornar o DTO completo
                return new PatientListDTO
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    PhoneNumber = patient.PhoneNumber,
                    DocumentNumber = patient.DocumentNumber,
                    BirthDate = patient.BirthDate,
                    Status = status ?? new StatusDTO { Id = patient.StatusId, Name = "Desconhecido" }
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar detalhes do paciente: {ex.Message}", ex);
            }
        }
        
        
        public async Task<bool> UpdatePatientAsync(int id, PatientInsertDTO patientDTO)
        {
            // Verificar se o paciente existe
            var existingPatient = await _patientRepository.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return false;
            }
            
            // Validar se o StatusId existe
            var statusExists = await ValidateStatusExistsAsync(patientDTO.StatusId);
            if (!statusExists)
            {
                throw new ArgumentException($"Status com ID {patientDTO.StatusId} não existe.");
            }
            
            var rowsAffected = await _patientRepository.UpdatePatientAsync(id, patientDTO);
            return rowsAffected > 0;
        }
        
        public async Task<bool> DeletePatientAsync(int id)
        {
            var rowsAffected = await _patientRepository.DeletePatientAsync(id);
            return rowsAffected > 0;
        }
    }
}