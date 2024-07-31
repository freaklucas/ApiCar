using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class FuelController(Context _context) : ControllerBase
{
    
    [HttpPost]
    public async Task<ActionResult<FuelRecord>> PostFuelRecord(FuelRecord fuelRecord)
    {
        if (fuelRecord is null) return BadRequest("Não possível criar registro de combustível.");
        
        _context.FuelRecords.Add(fuelRecord);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFuelRecord), new { id = fuelRecord.Id }, fuelRecord);
    }

    [HttpGet("{carId:int}")]
    public async Task<ActionResult<IEnumerable<FuelRecord>>> GetFuelRecord(int carId)
    {
        if (carId <= 0) return BadRequest("Não possível visualizar o registro.");

        var records = await _context
            .FuelRecords
            .Where(p => p.CarId == carId)
            .Include(c => c.Car)
            .AsTracking()
            .ToListAsync();

        if (!records.Any()) return NotFound("Não há registros de combustível para este carro.");

        return Ok(records);
    }
}