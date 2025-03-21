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
│   │   │   ├── Patient
│   │   │   │   ├── PatientDTO.cs
│   │   │   │   ├── PatientInsertDTO.cs
│   │   │   │   └── PatientListDTO.cs
│   │   │   ├── Specialty
│   │   │   │   ├── SpecialtyDTO.cs
│   │   │   │   └── SpecialtyInsertDTO.cs
│   │   │   └── Status
│   │   │       ├── StatusDTO.cs
│   │   │       └── StatusInsertDTO.cs
│   │   └── UseCases
│   │       ├── PatientUseCase.cs
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
│   │       ├── IPatientRepository.cs
│   │       ├── ISpecialtyRepository.cs
│   │       └── IStatusRepository.cs
│   ├── Infrastructure
│   │   └── Repositories
│   │       ├── PatientRepository.cs
│   │       ├── SpecialtyRepository.cs
│   │       └── StatusRepository.cs
│   └── WebAPI
│       └── Controllers
│           ├── PatientController.cs
│           ├── SpecialtyController.cs
│           └── StatusController.cs
└── structure
    └── ClinAgendaStructure.md

16 directories, 36 files