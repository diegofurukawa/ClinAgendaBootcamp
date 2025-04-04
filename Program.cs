using ClinAgendaBootcamp.src.Core.Interfaces;
using ClinAgendaBootcamp.src.Infrastructure.Repositories;
using ClinAgendaBootcamp.src.Application.UseCases;
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
builder.Services.AddScoped<IDoctorSpecialtyRepository, DoctorSpecialtyRepository>();
builder.Services.AddScoped<DoctorUseCase>();

// Appointment
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<AppointmentUseCase>();


// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
                    // .WithOrigins("http://localhost:3000") // Adicione a origem do seu front-end
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
