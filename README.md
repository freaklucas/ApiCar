## Estrutura a ser adaptada.

ApiCar/
│
├── Controllers/
│   ├── CarController.cs
│   ├── UserController.cs
│   ├── MaintenanceController.cs
│   └── InsurancePolicyController.cs
│
├── Data/
│   ├── Context.cs
│
├── Domain/
│   ├── Entities/
│   │   ├── Car.cs
│   │   ├── User.cs
│   │   ├── MaintenanceRecord.cs
│   │   └── InsurancePolicy.cs
│   │
│   ├── Enums/
│   │   └── TransmissionType.cs
│   │
│   ├── Interfaces/
│   │   ├── ICarRepository.cs
│   │   ├── IUserRepository.cs
│   │   ├── IMaintenanceRepository.cs
│   │   └── IInsurancePolicyRepository.cs
│   │
│   ├── Notifications/
│   │   ├── Notification.cs
│   │   └── Notifiable.cs
│
├── Infrastructure/
│   ├── Repositories/
│   │   ├── CarRepository.cs
│   │   ├── UserRepository.cs
│   │   ├── MaintenanceRepository.cs
│   │   └── InsurancePolicyRepository.cs
│
├── Services/
│   ├── CarService.cs
│   ├── UserService.cs
│   ├── MaintenanceService.cs
│   └── InsurancePolicyService.cs
│
├── Dtos/
│   ├── CarDto.cs
│   ├── UserDto.cs
│   ├── MaintenanceRecordDto.cs
│   └── InsurancePolicyDto.cs
│
├── Migrations/
│
├── obj/
│
├── Properties/
│
├── ApiCar.csproj
├── appsettings.Development.json
├── appsettings.json
└── Program.cs
