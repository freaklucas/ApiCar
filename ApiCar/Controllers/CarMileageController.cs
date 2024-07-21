using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class CarMileageController(Context context) : ControllerBase
{
    private readonly Context _context = context;

    [HttpGet("carId:int}")]
    public async Task<ActionResult<IEnumerable<CarMileage>>> GetCarMiles(int carId)
    {
        var mileages = await _context
            .CarMilesages
            .Where(c => c.CarId == carId)
            .AsTracking()
            .ToListAsync();

        if (!mileages.Any()) return NoContent();
        
        return Ok(mileages);
    }

    [HttpPost]
    public async Task<ActionResult<CarMileage>> PostCarMileage(CarMileage carMileage)
    {
        if(carMileage is null) return BadRequest("Quilometragem nulla.");
        
        var mileage = context.CarMilesages.Add(carMileage);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCarMiles), new { Id = carMileage.Id }, carMileage);
    }
}