# ClinAgenda

A medical appointment management API built with .NET Core to help clinics manage doctors, specialties, patients, and appointment scheduling.

## Overview

ClinAgenda is a complete solution for medical clinics to manage patient information, doctor specialties, and appointment scheduling. The system allows you to track patient status, doctor specialties, and maintain appointment records.

## Features

- **Patient Management**: Create, read, update, and delete patient records
- **Doctor Management**: Manage doctor information, statuses, and specialties
- **Specialty Management**: Track medical specialties and their schedule durations
- **Appointment Handling**: Schedule, update, and cancel appointments
- **Status Tracking**: Track entity status across the system

## Tech Stack

- **Backend**: .NET Core API
- **Database**: MySQL
- **ORM**: Dapper for efficient data access
- **Documentation**: Swagger/OpenAPI

## Project Structure

The application follows a clean architecture pattern:

```
ClinAgenda/
├── src/
│   ├── Application/          # Application business logic
│   │   ├── DTOs/             # Data Transfer Objects
│   │   └── UseCases/         # Business use cases
│   ├── Core/                 # Core business entities
│   │   ├── Entities/         # Domain entities
│   │   └── Interfaces/       # Repository interfaces
│   ├── Infrastructure/       # External dependencies
│   │   └── Repositories/     # Data access implementation
│   └── WebAPI/               # API controllers
│       └── Controllers/      # API endpoints
└── scripts/                  # Utility scripts
```

## Database Structure

The database consists of the following main tables:

- **Patient**: Stores patient information
- **Doctor**: Stores doctor information
- **Specialty**: Manages medical specialties
- **DoctorSpecialty**: Links doctors with their specialties
- **Appointment**: Tracks scheduled appointments
- **Status**: Stores status information for entities

## Getting Started

### Prerequisites

- .NET 6.0 SDK or higher
- MySQL Server
- Git

### Installation

1. Clone the repository:

```bash
git clone https://github.com/yourusername/ClinAgendaBootcamp.git
cd ClinAgendaBootcamp
```

2. Update the connection string in `appsettings.json` and `appsettings.Development.json` with your MySQL credentials:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=127.0.0.1;Database=clinagenda_database_bootcamp;User=your_user;Password=your_password;"
}
```

3. Create the MySQL database:

```sql
CREATE DATABASE clinagenda_database_bootcamp;
```

4. Run the application:

```bash
dotnet run
```

5. Access the API documentation at:

```
https://localhost:5001/swagger
```

## API Endpoints

### Patient Endpoints

- **GET /api/Patient/list**: Get all patients with optional filtering
- **GET /api/Patient/listById/{id}**: Get patient by ID
- **POST /api/Patient/insert**: Create a new patient
- **PUT /api/Patient/update/{id}**: Update existing patient
- **DELETE /api/Patient/delete/{id}**: Delete a patient

### Specialty Endpoints

- **GET /api/Specialty/list**: Get all specialties
- **GET /api/Specialty/listById/{id}**: Get specialty by ID
- **POST /api/Specialty/insert**: Create a new specialty

### Status Endpoints

- **GET /api/status/list**: Get all statuses
- **GET /api/status/listById/{id}**: Get status by ID
- **POST /api/status/insert**: Create a new status

## Utility Scripts

The project includes useful utility scripts:

- **search.sh**: Search for text within project files
- **backup.sh**: Create backups of the project

### Using search.sh

```bash
./scripts/search.sh "text_to_search" ["file_extension"] ["directory"]
```

Example:
```bash
./scripts/search.sh "Patient" "cs" "src"
```

### Using backup.sh

```bash
./backup.sh
```

## Development

### Adding a New Entity

1. Create entity class in `src/Core/Entities`
2. Create DTOs in `src/Application/DTOs`
3. Define repository interface in `src/Core/Interfaces`
4. Implement repository in `src/Infrastructure/Repositories`
5. Create use case in `src/Application/UseCases`
6. Create controller in `src/WebAPI/Controllers`
7. Register dependencies in `Program.cs`

## Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m 'Add some feature'`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Thanks to everyone who contributed to the ClinAgenda bootcamp project
