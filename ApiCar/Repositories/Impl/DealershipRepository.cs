using ApiCar.Data;
using ApiCar.Models;
using ApiCar.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Repositories.Impl;

public class DealershipRepository(Context context ) : IDealershipRepository
{
    private readonly Context _context = context;
    
    public async Task<IEnumerable<Dealership>> GetAll()
    {
        var dealerships = await _context
            .Dealerships
            .Include(p => p.CarListings)
            .AsNoTracking()
            .ToListAsync() ?? throw new InvalidOperationException("Nenhuma concessionária foi encontrada.");
        return dealerships;
    }

    public async Task<Dealership> GetById(int id)
    {
        ValidateId(id);

        var dealership = await _context
            .Dealerships
            .Include(p => p.CarListings)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException("Nenhuma concessionária foi encontrada.");

        return dealership;
    }

    public async Task Create(Dealership dealership)
    {
        if(dealership == null) throw new ArgumentNullException(nameof(dealership), "Houve um erro ao tentar cadastrar.");
        
        _context.Dealerships.Add(dealership);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Dealership dealership, int id)
    {
        ValidateId(id);
        
        if (dealership == null) throw new ArgumentNullException(nameof(dealership), "Houve um erro ao tentar atualizar.");
        
        if(dealership.Id != id) throw new KeyNotFoundException("Id's diferentes.");

        var dealerShipDto = await _context
            .Dealerships
            .FirstOrDefaultAsync(e => e.Id == id);

        dealerShipDto.Name = dealership.Name;
        dealerShipDto.Address = dealership.Address;
        dealerShipDto.City = dealership.City;
        dealerShipDto.Contact = dealership.Contact;
        
        // _context.Dealerships.Update(dealership);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        ValidateId(id);
        
        var dealership = await _context.Dealerships.FindAsync(id);
        
        if (dealership == null) throw new KeyNotFoundException("Concessionária não encontrada.");
        
        _context.Dealerships.Remove(dealership);
        
        await _context.SaveChangesAsync();
    }

    private void ValidateId(int id)
    {
        if (id <= 0) throw new ArgumentException("Id inválido", nameof(id));
    }
}