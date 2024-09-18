using ApiCar.Models;
using ApiCar.Repositories.Interface;
using ApiCar.Services.Interface;

namespace ApiCar.Services.Impl;

public class DealershipService(IDealershipRepository dealershipRepository) : IDealershipService
{
    private readonly IDealershipRepository _dealershipRepository = dealershipRepository;
    
    public async Task<IEnumerable<Dealership>> GetAllDealerships()
    {
        return await _dealershipRepository.GetAll();
    }

    public async Task<Dealership> GeDealershiptById(int id)
    {
        return await _dealershipRepository.GetById(id);
    }

    public async Task CreateDealership(Dealership dealership)
    {
        await _dealershipRepository.Create(dealership);
    }

    public async Task UpdateDealership(Dealership dealership, int id)
    {
        await _dealershipRepository.Update(dealership, id);
    }

    public async Task DeleteDealership(int id)
    {
        await _dealershipRepository.Delete(id);
    }
}