
// Importacao dos UseCases
using ClinAgendaBootcamp.src.Application.PatientUseCase;
using ClinAgendaBootcamp.src.Application.SpecialtyUseCase;
using ClinAgendaBootcamp.src.Application.DoctorUseCase;
using ClinAgendaBootcamp.src.Application.StatusUseCase;

// Importacao das Interfaces
using ClinAgendaBootcamp.src.Core.Interfaces;

// Importacao dos Repositorios
using ClinAgendaBootcamp.src.Infrastructure.Repositories;

// Importacao do MYSQL
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Configuração da conexão com MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<MySqlConnection>(_ => new MySqlConnection(connectionString));

// Status
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<StatusUseCase>();

// Specialty
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<SpecialtyUseCase>();


// Patient
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<PatientUseCase>();

// Doctor
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorSpecialtyRepository, DoctorSpecialtyRepository>(); // Add this line
builder.Services.AddScoped<DoctorUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
