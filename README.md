## Fases do projeto

```
- [x] Validações
- [x] Criar um produto mínimo viável
- [ ] Adicionar testes unitários (xUnit)
- [ ] Organizar arquitetura seguindo a documentação que criei com base em vozes da minha cabeça

```


## Ideia de incremento
```
ApiCar/
│
├── Controllers/
│   ├── CarController.cs
│   ├── CarListingController.cs
│   ├── CarMileageController.cs
│   ├── ChangeLogController.cs
│   ├── FuelController.cs
│   ├── InsurancePolicyController.cs
│   ├── MaintenanceController.cs
│   ├── ReportsController.cs
│   ├── UserController.cs
│   └── VehicleReportController.cs
│
├── Data/
│   ├── Context.cs
│
├── Domain/
│   ├── Entities/
│   │   ├── Car.cs
│   │   ├── User.cs
│   │   ├── MaintenanceRecord.cs
│   │   ├── InsurancePolicy.cs
│   │   ├── CarMileage.cs
│   │   └── VehicleReport.cs
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
│   ├── InsurancePolicyService.cs
│   └── MaintenanceAlertService.cs
│
├── Dtos/
│   ├── CarDto.cs
│   ├── UserDto.cs
│   ├── MaintenanceRecordDto.cs
│   ├── InsurancePolicyDto.cs
│   ├── YearlyCostDto.cs
│   └── LoanSimulationDto.cs
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


```