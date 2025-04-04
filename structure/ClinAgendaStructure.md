.
├── appsettings.Development.json
├── appsettings.json
├── ClinAgendaBootcamp.csproj
├── ClinAgendaBootcamp.http
├── ClinAgendaBootcamp.sln
├── Program.cs
├── Properties
│   └── launchSettings.json
├── README.md
├── scripts
│   └── search.sh
├── src
│   ├── Application
│   │   ├── DTOs
│   │   │   ├── Appointment
│   │   │   │   ├── AppointmentDoctorReturnDTO.cs
│   │   │   │   ├── AppointmentDTO.cs
│   │   │   │   ├── AppointmentInsertDTO.cs
│   │   │   │   ├── AppointmentListDTO.cs
│   │   │   │   ├── AppointmentListReturnDTO.cs
│   │   │   │   ├── AppointmentPatientReturnDTO.cs
│   │   │   │   └── AppointmentResponseDTO.cs
│   │   │   ├── Doctor
│   │   │   │   ├── DoctorDTO.cs
│   │   │   │   ├── DoctorInsertDTO.cs
│   │   │   │   ├── DoctorListDTO.cs
│   │   │   │   ├── DoctorListReturnDTO.cs
│   │   │   │   ├── DoctorResponseDTO.cs
│   │   │   │   ├── DoctorReturnAppointmentDTO.cs
│   │   │   │   ├── DoctorSpecialtyDTO.cs
│   │   │   │   └── SpecialtyDoctorDTO.cs
│   │   │   ├── Patient
│   │   │   │   ├── PatientDTO.cs
│   │   │   │   ├── PatientInsertDTO.cs
│   │   │   │   ├── PatientListDTO.cs
│   │   │   │   ├── PatientListReturnDTO.cs
│   │   │   │   ├── PatientResponseDTO.cs
│   │   │   │   └── PatientReturnAppointmentDTO.cs
│   │   │   ├── Specialty
│   │   │   │   ├── SpecialtyDTO.cs
│   │   │   │   └── SpecialtyInsertDTO.cs
│   │   │   └── Status
│   │   │       ├── StatusDTO.cs
│   │   │       └── StatusInsertDTO.cs
│   │   └── UseCases
│   │       ├── AppointmentUseCase.cs
│   │       ├── DoctorUseCase.cs
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
│   │   │   ├── Specialty.cs
│   │   │   ├── Status.cs
│   │   │   └── Test.cs
│   │   └── Interfaces
│   │       ├── IAppointmentRepository.cs
│   │       ├── IDoctorRepository.cs
│   │       ├── IDoctorSpecialtyRepository.cs
│   │       ├── IPatientRepository.cs
│   │       ├── ISpecialtyRepository.cs
│   │       └── IStatusRepository.cs
│   ├── Infrastructure
│   │   └── Repositories
│   │       ├── AppointmentRepository.cs
│   │       ├── DoctorRepository.cs
│   │       ├── DoctorSpecialtyRepository.cs
│   │       ├── PatientRepository.cs
│   │       ├── SpecialtyRepository.cs
│   │       └── StatusRepository.cs
│   └── WebAPI
│       └── Controllers
│           ├── AppointmentController.cs
│           ├── DoctorController.cs
│           ├── PatientController.cs
│           ├── SpecialtyController.cs
│           └── StatusController.cs
└── structure
    └── ClinAgendaStructure.md

19 directories, 65 files