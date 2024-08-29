using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("/api/[controller]")]
[ApiController]

public class CarListingController : ControllerBase
{
    private readonly Context _context;
    
    public CarListingController(Context context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarListing>>> GetAllListings()
    {
        var listing = await _context
            .CarListings
            .AsTracking()
            .ToListAsync();

        if (!listing.Any()) return NoContent();
        
        return Ok(listing);
    }

    [HttpGet("{carId:int}")]
    public async Task<ActionResult<IEnumerable<CarListing>>> GetListings(int carId)
    {
        var listing = await _context
            .CarListings
            .Where(c => c.CarId == carId)
            .AsTracking()
            .ToListAsync();

        if (!listing.Any()) return NoContent();
        
        return Ok(listing);
    }

    [HttpPost]
    public async Task<ActionResult<CarListing>> PostCarListing(CarListing carListing)
    {
        if (carListing is null) return BadRequest("Dados inválidos.");
    
        var carExists = await _context.Cars.AnyAsync(c => c.Id == carListing.CarId);
        if (!carExists) return BadRequest("Carro não encontrado.");

        _context.CarListings.Add(carListing);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetListings), new { carListing.CarId }, carListing);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CarListing>> PutCarListing(int id, CarListing carListing)
    {
        if(id <= 0) return BadRequest("Id inválido.");
        if(carListing is null) return BadRequest("Dados inválidos.");
        if (id != carListing.Id) return BadRequest("Id inválido.");

        var existing = await _context.CarListings.FindAsync(id);
        if (existing is null) return NotFound("Anúncio não encontrado.");
        
        existing.Description = carListing.Description;
        existing.Price = carListing.Price;
        existing.ListingDate = carListing.ListingDate;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest($"Falha ao atualizar o anúncio: {e.Message}");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCarListing(int id)
    {
        if (id <= 0) return BadRequest("Não foi possível atender a solicitação.");

        var findCarListing = await _context.CarListings
            .Include(cl => cl.Car)
            .FirstOrDefaultAsync(cl => cl.Id == id);

        if (findCarListing is null) return NotFound("Anúncio não encontrado.");

        LogDeletion(findCarListing, findCarListing?.Car?.Model);

        _context.CarListings.Remove(findCarListing);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private void LogDeletion<TEntity>(TEntity entity, string userName) where TEntity : class
    {
        var entry = _context.Entry(entity);

        if (entity is null || userName is null) return;

        foreach (var property in entry.OriginalValues.Properties)
        {
            var originalValue = entry.OriginalValues[property]?.ToString();
            var log = new ChangeLog
            {
                EntityName = typeof(TEntity).Name,
                EntityId = (int)entry.Property("Id").CurrentValue,
                PropertyName = property.Name,
                OldValue = originalValue,
                NewValue = "DELETED",
                ChangeDate = DateTime.Now,
                UserName = userName
            };
            _context.ChangeLogs.Add(log);
        }
    }

}