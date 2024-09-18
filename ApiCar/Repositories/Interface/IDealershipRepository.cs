using ApiCar.Models;

namespace ApiCar.Repositories.Interface;

public interface IDealershipRepository
{
    Task<IEnumerable<Dealership>> GetAll();
    Task<Dealership> GetById(int id);
    Task Add(Dealership dealership);
    Task Update(Dealership dealership, int id);
    Task Delete(int id);
}