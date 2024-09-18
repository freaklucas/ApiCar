using ApiCar.Models;

namespace ApiCar.Services.Interface;

public interface IDealershipService
{
    Task<IEnumerable<Dealership>> GetAllDealerships();
    Task<Dealership> GeDealershiptById(int id);
    Task CreateDealership(Dealership dealership);
    Task UpdateDealership(Dealership dealership, int id);
    Task DeleteDealership(int id);
}