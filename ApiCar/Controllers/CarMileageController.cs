using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarMileageController : ControllerBase
{
    private readonly Context _context;

    public CarMileageController(Context context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<CarMileage>> PostCarMileage(CarMileage carMileage)
    {
        if (carMileage is null) return BadRequest("Quilometragem nula.");

        _context.CarMilesages.Add(carMileage);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCarMiles), new { id = carMileage.Id }, carMileage);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarMileage>> GetCarMiles(int id)
    {
        var carMileage = await _context.CarMilesages.FindAsync(id);

        if (carMileage is null) return NotFound();

        return Ok(carMileage);
    }

    [HttpGet("GetCarMileage/{id:int}")]
    public async Task<ActionResult<CarMileage>> GetCarMileage(int id)
    {
        if (id <=0) return BadRequest("ID inválido.");
        var carMileage = await _context
            .CarMilesages
            .Include(c => c.Car)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (carMileage is null) return BadRequest("Não foi possível encontrar uma quilometragem.");
        
        return Ok(carMileage);
    }
}