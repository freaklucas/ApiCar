using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class MaintenanceController(Context context) : ControllerBase
{
    private readonly Context _context = context;

    [HttpGet("{carId:int}")]
    public async Task<ActionResult<IEnumerable<MaintenanceRecord>>> GetMaintenanceRecords(int carId)
    {
        var findMaintenance = await _context
            .MaintenanceRecords
            .Where(m => m.CarId == carId)
            .AsTracking()
            .ToListAsync();
        
        if(!findMaintenance.Any()) return BadRequest("Não foi possível encontrar um veículo com manutenção.");

        return Ok(findMaintenance);
    }
    
    [HttpGet("/DataManutencao/")]
    public async Task<ActionResult<IEnumerable<MaintenanceRecord>>> GetDateMaintenance(DateTime startDate, DateTime endDate)
    {
        var findMaintenance = await _context
            .MaintenanceRecords
            .Where(m => m.Date >= startDate && m.Date <= endDate)
            .AsTracking()
            .ToListAsync();
        
        if (!findMaintenance.Any()) return BadRequest("Não foi encontrado data de manutenção.");
        
        return Ok(findMaintenance);
    }
    
    
    [HttpPost]
    public async Task<ActionResult<MaintenanceRecord>> 
        PostMaintenanceRecord(MaintenanceRecord maintenanceRecord)
    {
        if (maintenanceRecord is null) return BadRequest("Não foi possível criar uma manutenção.");
        
        _context.MaintenanceRecords.Add(maintenanceRecord);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMaintenanceRecords), 
            new { carId = maintenanceRecord.CarId }, maintenanceRecord);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutMaintenanceRecord(int id, MaintenanceRecord maintenanceRecord)
    {
        if(id != maintenanceRecord.Id) return BadRequest("Ids não correspondem.");
        if(maintenanceRecord is null) return BadRequest("Não foi possivel atualizar manutenção do veículo.");

        var existingMaintenanceRecord = await _context.MaintenanceRecords.FindAsync(id);
        if (existingMaintenanceRecord is null) return BadRequest("Não foi possivel atualizar.");
        
        existingMaintenanceRecord.CarId = maintenanceRecord.CarId;
        existingMaintenanceRecord.Cost = maintenanceRecord.Cost;
        existingMaintenanceRecord.Date = existingMaintenanceRecord.Date;
        existingMaintenanceRecord.Description = existingMaintenanceRecord.Description;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteMaintenanceRecord(int id)
    {
        if(id <= 0) return BadRequest("Registro de  manutenção não foi encontrada.");
        var maintenanceRecord = await _context.MaintenanceRecords.FindAsync(id);
        if (maintenanceRecord is null) return BadRequest("Registro de  manutenção não foi encontrada.");

        _context.MaintenanceRecords.Remove(maintenanceRecord);
        await _context.SaveChangesAsync();

        return Accepted(maintenanceRecord);
    }
    
}