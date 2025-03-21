using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;
using ClinAgendaBootcamp.src.Application.DTOs.Status;
using ClinAgendaBootcamp.src.Core.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace ClinAgendaBootcamp.src.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        // Conexão com o banco pode ser acessada pelo UseCase para validações
public readonly MySqlConnection _connection;

        public PatientRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            string query = @"
                SELECT 
                    p.id,
                    p.name,
                    p.phonenumber,
                    p.documentnumber,
                    p.birthdate,
                    p.statusid
                FROM patient p
                WHERE p.id = @Id";

            var parameters = new { Id = id };
            
            var patient = await _connection.QueryFirstOrDefaultAsync<PatientDTO>(query, parameters);
            
            return patient;
        }

        public async Task<int> DeletePatientAsync(int id)
        {
            string query = @"
                DELETE FROM patient
                WHERE id = @Id";

            var parameters = new { Id = id };
            
            return await _connection.ExecuteAsync(query, parameters);
        }

        public async Task<int> InsertPatientAsync(PatientInsertDTO patientInsertDTO)
        {
            try
            {
                // Verificar e normalizar a data se necessário
                string normalizedDate = patientInsertDTO.BirthDate;
                if (DateTime.TryParse(patientInsertDTO.BirthDate, out DateTime birthDate))
                {
                    normalizedDate = birthDate.ToString("yyyy-MM-dd");
                }
                
                // Preparar os parâmetros para garantir o formato correto
                var parameters = new
                {
                    patientInsertDTO.Name,
                    patientInsertDTO.PhoneNumber,
                    patientInsertDTO.DocumentNumber,
                    BirthDate = normalizedDate,
                    patientInsertDTO.StatusId
                };
                
                string query = @"
                    INSERT INTO patient (name, phonenumber, documentnumber, birthdate, statusid) 
                    VALUES (@Name, @PhoneNumber, @DocumentNumber, @BirthDate, @StatusId);
                    SELECT LAST_INSERT_ID();";
                    
                return await _connection.ExecuteScalarAsync<int>(query, parameters);
            }
            catch (Exception ex)
            {
                // Melhorar a mensagem de erro para capturar problemas relacionados à data
                if (ex.Message.Contains("Incorrect date value") || ex.Message.Contains("birthdate"))
                {
                    throw new Exception($"Formato de data inválido para o campo birthDate. Use o formato YYYY-MM-DD. Erro original: {ex.Message}");
                }
                throw;
            }
        }

        public async Task<int> UpdatePatientAsync(int id, PatientInsertDTO patientInsertDTO)
        {
            string query = @"
                UPDATE patient 
                SET name = @Name, 
                    phonenumber = @PhoneNumber, 
                    documentnumber = @DocumentNumber, 
                    birthdate = @BirthDate, 
                    statusid = @StatusId
                WHERE id = @Id";

            var parameters = new
            {
                Id = id,
                patientInsertDTO.Name,
                patientInsertDTO.PhoneNumber,
                patientInsertDTO.DocumentNumber,
                patientInsertDTO.BirthDate,
                patientInsertDTO.StatusId
            };
            
            return await _connection.ExecuteAsync(query, parameters);
        }

        public async Task<(int total, IEnumerable<PatientDTO> patients)> GetAllAsync(int? itemsPerPage, int? page)
        {
            var queryBase = new StringBuilder(@"
                FROM patient p WHERE 1 = 1");

            var parameters = new DynamicParameters();

            // Count total records
            var countQuery = $"SELECT COUNT(DISTINCT p.id) {queryBase}";
            int total = await _connection.ExecuteScalarAsync<int>(countQuery, parameters);

            // Get paginated data
            var dataQuery = $@"
                SELECT 
                    p.id,
                    p.name,
                    p.phonenumber,
                    p.documentnumber,
                    p.birthdate,
                    p.statusid
                {queryBase}
                ORDER BY p.id
                LIMIT @Limit OFFSET @Offset";

            parameters.Add("Limit", itemsPerPage);
            parameters.Add("Offset", (page - 1) * itemsPerPage);

            var patients = await _connection.QueryAsync<PatientDTO>(dataQuery, parameters);

            return (total, patients);
        }
        
        public async Task<PatientListDTO> GetPatientDetailsAsync(int id)
        {
            string query = @"
                SELECT 
                    p.id,
                    p.name,
                    p.phonenumber,
                    p.documentnumber,
                    p.birthdate,
                    s.id as 'Status.Id',
                    s.name as 'Status.Name'
                FROM patient p
                LEFT JOIN status s ON s.id = p.statusid
                WHERE p.id = @Id";

            var parameters = new { Id = id };
            
            PatientListDTO result = null;
            
            // Usando um método diferente de mapeamento que lida melhor com a relação
            var patientDictionary = new Dictionary<int, PatientListDTO>();
            
            await _connection.QueryAsync<PatientListDTO, StatusDTO, PatientListDTO>(
                query,
                (patient, status) => {
                    if (!patientDictionary.TryGetValue(patient.Id, out PatientListDTO patientEntry))
                    {
                        patientEntry = patient;
                        patientDictionary.Add(patient.Id, patientEntry);
                    }
                    
                    patientEntry.Status = status;
                    return patientEntry;
                },
                parameters,
                splitOn: "Status.Id"
            );
            
            result = patientDictionary.Values.FirstOrDefault();
            return result;
        }

        public Task<(int total, IEnumerable<PatientListDTO> patients)> GetAllWithDetailsAsync(int? itemsPerPage, int? page)
        {
            throw new NotImplementedException();
        }
    }
}