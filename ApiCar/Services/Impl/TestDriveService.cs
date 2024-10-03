using ApiCar.Data;
using ApiCar.Models;
using ApiCar.Repositories.Interface;
using ApiCar.Services.Interface;

namespace ApiCar.Services.Impl;

public class TestDriveService(
    ITestDriveRepository testDriveRepository,
    Context context,
    IDealershipRepository dealershipRepository) : ITestDriveService
{
    private readonly ITestDriveRepository _testDriveRepository = testDriveRepository;
    private readonly Context _context = context;
    private readonly IDealershipRepository _dealershipRepository = dealershipRepository;

    public async Task<IEnumerable<TestDrive>> GetAllTestsDrive()
    {
        return await testDriveRepository.GetAll();
    }

    public async Task<TestDrive> GetTestDriveById(int id)
    {
        return await testDriveRepository.GetById(id);
    }

    public async Task CreateTestDrive(TestDrive testDrive)
    {
        await ValidateIds(testDrive);

        await testDriveRepository.Create(testDrive);
    }

    public async Task UpdateTestDrive(TestDrive testDrive, int id)
    {
        await ValidateIds(testDrive);

        await testDriveRepository.Update(testDrive, id);
    }

    public async Task DeleteTestDrive(int id)
    {
        await testDriveRepository.Delete(id);
    }

    private async Task ValidateIds(TestDrive testDrive)
    {
        var carExists = await _context.Cars.FindAsync(testDrive.CarId);
        if (carExists == null) throw new ArgumentException("Carro não encontrado.");

        var userExists = await _context.Users.FindAsync(testDrive.UserId);
        if (userExists == null) throw new ArgumentException("Pessoa não encontrada.");

        var dealershipExists = await _dealershipRepository.GetById(testDrive.DealershipId);
        if (dealershipExists == null) throw new ArgumentException("Concessionária não encontrada.");
    }
}