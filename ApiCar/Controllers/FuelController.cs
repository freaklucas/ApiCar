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

        return CreatedAtAction(nameof(GetFuelRecordById), new { id = fuelRecord.Id }, fuelRecord);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FuelRecord>> GetFuelRecordById(int id)
    {
        if (id <= 0) return BadRequest("Id inválido.");

        var record = await _context
            .FuelRecords
            .Include(c => c.Car)
            .AsTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        if (record == null) return NotFound("Registro não encontrado.");

        return Ok(record);
    }

    [HttpGet("car/{carId:int}")]
    public async Task<ActionResult<IEnumerable<FuelRecord>>> GetFuelRecords(int carId)
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

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutFuel(int id, FuelRecord fuelRecord)
    {
        if (id <= 0) return BadRequest("Não foi possível atender a solicitação.");
        if (fuelRecord is null) return BadRequest("Entidade incompleta.");
        if (id != fuelRecord.Id) return BadRequest("Entidade incompleta, ids diferem.");

        var findFuel = await _context.FuelRecords.FindAsync(id);
        if (findFuel is null) return NotFound("Registro não encontrado.");
    
        findFuel.Price = fuelRecord.Price;
        findFuel.Date = fuelRecord.Date;
        findFuel.Mileage = fuelRecord.Mileage;
        findFuel.Quantity = fuelRecord.Quantity;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest($"Não foi possível atender a solicitação {e.Message}");
        }
        
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteFuel(int id)
    {
        if (id <= 0) return BadRequest("Não foi possível atender a solicitação.");

        var findFuel = await _context
            .FuelRecords
            .Include(f => f.Car)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (findFuel is null) return NotFound("Registro não encontrado.");

        // Registrar a remoção explícita no log
        LogDeletion(findFuel, findFuel.Car?.Model);

        _context.FuelRecords.Remove(findFuel);
    
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

        _context.SaveChanges();
    }

}