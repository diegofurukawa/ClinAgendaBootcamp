using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinAgendaBootcamp.src.Application.DTOs.Patient;
using ClinAgendaBootcamp.src.Core.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;


namespace ClinAgendaBootcamp.src.Infrastructure.Repositories
{
    // Implementação do repositório de Patient, seguindo a interface IPatientRepository.
    public class PatientRepository : IPatientRepository
    {
        private readonly MySqlConnection _connection; // Conexão com o banco de dados.

        // Construtor que recebe a conexão com o banco de dados via injeção de dependência.
        public PatientRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        // Método assíncrono para buscar um Patient pelo ID.
        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            // Query SQL para selecionar o Patient pelo ID.
            string query = @"
            SELECT ID, 
                   NAME 
            FROM patient
            WHERE ID = @Id";

            var parameters = new { Id = id }; // Parâmetro da query.

            // Executa a consulta no banco e retorna o primeiro resultado ou null caso não encontre.
            var patient = await _connection.QueryFirstOrDefaultAsync<PatientDTO>(query, parameters);

            return patient; // Retorna o Patient encontrado.
        }

        // Método assíncrono para excluir um Patient pelo ID.
        public async Task<int> DeletePatientAsync(int id)
        {
            // Query SQL para deletar um Patient pelo ID.
            string query = @"
            DELETE FROM patient
            WHERE ID = @Id";

            var parameters = new { Id = id }; // Parâmetro da query.

            // Executa a query e retorna o número de linhas afetadas.
            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            return rowsAffected; // Retorna quantas linhas foram afetadas (1 se deletou, 0 se não encontrou o ID).
        }

        // Método assíncrono para inserir um novo Patient no banco.
        public async Task<int> InsertPatientAsync(PatientInsertDTO PatientInsertDTO)
        {
            // Query SQL para inserir um novo Patient e obter o ID gerado.
            string query = @"
            INSERT INTO patient (NAME) 
            VALUES (@Name);
            SELECT LAST_INSERT_ID();"; // Obtém o ID do último registro inserido.

            // Executa a query e retorna o ID do novo Patient.
            return await _connection.ExecuteScalarAsync<int>(query, PatientInsertDTO);
        }

        // Método assíncrono para obter todos os Patient com paginação.
        public async Task<(int total, IEnumerable<PatientDTO> patients)> GetAllAsync(int? itemsPerPage, int? page)
        {
            // Construção dinâmica da query base.
            var queryBase = new StringBuilder(@"
                FROM patient S WHERE 1 = 1"); // "1 = 1" é usado para facilitar adição de filtros dinâmicos.

            var parameters = new DynamicParameters(); // Objeto para armazenar os parâmetros da query.

            // Query para contar o número total de registros sem a paginação.
            var countQuery = $"SELECT COUNT(DISTINCT S.ID) {queryBase}";
            int total = await _connection.ExecuteScalarAsync<int>(countQuery, parameters);

            // Query para buscar os dados paginados.
            var dataQuery = $@"
            SELECT ID, 
            NAME
            {queryBase}
            LIMIT @Limit OFFSET @Offset";

            // Adiciona os parâmetros de paginação.
            parameters.Add("Limit", itemsPerPage);
            parameters.Add("Offset", (page - 1) * itemsPerPage);

            // Executa a consulta e retorna os resultados.
            var patient = await _connection.QueryAsync<PatientDTO>(dataQuery, parameters);

            return (total, patient); // Retorna o total de registros e a lista de Patient paginada.
        }

        // public Task<int> DeletePatientAsync(int id)
        // {
        //     throw new NotImplementedException();
        // }

        // public Task<int> InsertPatientAsync(PatientInsertDTO patientInsertDTO)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
