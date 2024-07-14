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
}