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

    [HttpGet("{carId:int}")]
    public async Task<ActionResult<IEnumerable<CarListing>>> GetListings(int carId)
    {
        var listing = await _context
            .CarListings
            .Where(c => c.CarId == carId)
            .ToListAsync();

        if (!listing.Any()) return NoContent();
        
        return Ok(listing);
    }

    [HttpPost]
    public async Task<ActionResult<CarListing>> PostCarListing(CarListing carListing)
    {
        if(carListing is null) return NoContent();

        _context.CarListings.Add(carListing);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetListings), new { carListing = carListing.CarId }, carListing);
    }
}