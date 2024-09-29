using ApiCar.Models;
using ApiCar.Repositories.Interface;
using ApiCar.Services.Interface;

namespace ApiCar.Services.Impl;

public class TestDriveService(ITestDriveRepository testDriveRepository) : ITestDriveService
{
    public async Task<IEnumerable<TestDrive>> GetAllTestsDrive() 
        => await testDriveRepository.GetAll();

    public async Task<TestDrive> GetTestDriveById(int id)
        => await testDriveRepository.GetById(id);

    public async Task CreateTestDrive(TestDrive testDrive)
        => await testDriveRepository.Create(testDrive);

    public async Task UpdateTestDrive(TestDrive testDrive, int id)
        => await testDriveRepository.Update(testDrive, id);

    public async Task DeleteTestDrive(int id)
        => await testDriveRepository.Delete(id);
}