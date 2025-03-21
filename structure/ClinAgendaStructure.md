.
├── appsettings.Development.json
├── appsettings.json
├── ClinAgendaBootcamp.csproj
├── ClinAgendaBootcamp.http
├── ClinAgendaBootcamp.sln
├── Program.cs
├── scripts
│   └── search.sh
├── src
│   ├── Application
│   │   ├── DTOs
│   │   │   ├── Specialty
│   │   │   │   ├── SpecialtyDTO.cs
│   │   │   │   └── SpecialtyInsertDTO.cs
│   │   │   └── Status
│   │   │       ├── StatusDTO.cs
│   │   │       └── StatusInsertDTO.cs
│   │   └── UseCases
│   │       ├── SpecialtyUseCase.cs
│   │       └── StatusUseCase.cs
│   ├── Core
│   │   ├── Entities
│   │   │   ├── Appointment.cs
│   │   │   ├── Doctor.cs
│   │   │   ├── DoctorSpeciality.cs
│   │   │   ├── DoctorSpecialty.cs
│   │   │   ├── Patient.cs
│   │   │   ├── Speciality.cs
│   │   │   ├── Specialty.cs
│   │   │   ├── Status.cs
│   │   │   └── Test.cs
│   │   └── Interfaces
│   │       ├── ISpecialtyRepository.cs
│   │       └── IStatusRepository.cs
│   ├── Infrastructure
│   │   └── Repositories
│   │       ├── SpecialtyRepository.cs
│   │       └── StatusRepository.cs
│   └── WebAPI
│       └── Controllers
│           ├── SpecialtyController.cs
│           └── StatusController.cs
└── structure
    └── ClinAgendaStructure.md

15 directories, 29 files