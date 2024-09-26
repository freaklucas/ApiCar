using ApiCar.Models;

namespace ApiCar.Repositories.Interface;

public interface ITestDriveRepository
{
    Task<IEnumerable<TestDrive>> GetAll();
    Task<TestDrive> GetById (int id);
    Task Create(TestDrive testDrive);
    Task Update(TestDrive? testDrive, int id);
    Task Delete(int id);
}