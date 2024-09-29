using ApiCar.Models;

namespace ApiCar.Services.Interface;

public interface ITestDriveService
{
    Task<IEnumerable<TestDrive>> GetAllTestsDrive();
    Task<TestDrive> GetTestDriveById(int id);
    Task CreateTestDrive(TestDrive testDrive);
    Task UpdateTestDrive(TestDrive testDrive, int id);
    Task DeleteTestDrive(int id);
}