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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarMileage>>> Get()
    {
        var result = await _context
            .CarMilesages
            .Include(c => c.Car)
            .AsTracking()
            .ToListAsync();
        
        if (result is null) return BadRequest("Não existem quilometragens registradas.");

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CarMileage>> PostCarMileage(CarMileage carMileage)
    {
        if (carMileage is null) return BadRequest("Quilometragem nula.");

        _context.CarMilesages.Add(carMileage);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCarMile), new { id = carMileage.Id }, carMileage);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CarMileage>> PutMileage(int id, CarMileage carMileage)
    {
        if (id <= 0) return BadRequest("Id inválido.");
        if (carMileage is null) return BadRequest("Quilometragem nula.");

        var findMileage = await _context.CarMilesages.FindAsync(id);
        if (findMileage is null) return BadRequest("Quilometragem não encontrada.");

        findMileage.Mileage = carMileage.Mileage;
        findMileage.Date = carMileage.Date;

        try
        {
            await _context.SaveChangesAsync();
        }

        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMileage(int id)
    {
        if (id <= 0) return BadRequest("Id inexistente.");

        var mileage = await _context.CarMilesages.FindAsync(id);
        if (mileage is null) return BadRequest("Quilometragem não encontrada.");

        _context.CarMilesages.Remove(mileage);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarMileage>> GetCarMile(int id)
    {
        var carMileage = await _context
            .CarMilesages
            .Include(c => c.Car)
            .AsTracking()
            .OrderByDescending(m => m.Date)
            .FirstOrDefaultAsync(c => c.CarId == id);

        if (carMileage is null) return NotFound();

        return Ok(carMileage);
    }

    [HttpGet("GetCarMileage/{id:int}")]
    public async Task<ActionResult<CarMileage>> GetCarMileage(int id)
    {
        if (id <= 0) return BadRequest("ID inválido.");
        var carMileage = await _context
            .CarMilesages
            .Include(c => c.Car)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (carMileage is null) return BadRequest("Não foi possível encontrar uma quilometragem.");

        return Ok(carMileage);
    }
}