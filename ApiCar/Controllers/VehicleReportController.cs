using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleReportController(Context context) : ControllerBase
{
    private readonly Context _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleReport>>> GetVehicleReport()
    {
        var vehicles = await _context.VehicleReports
            .Include(p => p.CarListing!.Car)
            .AsNoTracking()
            .ToListAsync();

        if (!vehicles.Any()) return NoContent();

        return Ok(vehicles);
    }

    [HttpPost]
    public async Task<ActionResult> CreateVehicleReport(VehicleReport? vehicleReport)
    {
        if (vehicleReport == null) return BadRequest("Dados inválidos.");

        var carListingExists = await _context.CarListings.AnyAsync(c => c.Id == vehicleReport.CarListingId);
        if (!carListingExists) return BadRequest("CarListing não encontrado.");

        _context.VehicleReports.Add(vehicleReport);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetVehicleReport", new { id = vehicleReport.Id }, vehicleReport);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<VehicleReport>> UpdateVehicleReport(int id, [FromBody] VehicleReport? vehicleReport)
    {
        if (vehicleReport == null) return BadRequest("Dados inválidos.");
        if (vehicleReport.Id != id) return BadRequest("Dados inválidos.");

        var vehicleDto = await _context.VehicleReports.FindAsync(id);

        if (vehicleDto == null) return NotFound();

        // _context.Entry(vehicleReport).State = EntityState.Modified;

        vehicleDto.NumberReport = vehicleReport.NumberReport;
        vehicleDto.Chassi = vehicleReport.Chassi;
        vehicleDto.EngineNumber = vehicleReport.EngineNumber;
        vehicleDto.LicensePlate = vehicleReport.LicensePlate;
        vehicleDto.Make = vehicleReport.Make;
        vehicleDto.Mileage = vehicleReport.Mileage;
        vehicleDto.Color = vehicleReport.Color;
        vehicleDto.YearOfManufacture = vehicleReport.YearOfManufacture;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return BadRequest($"Erro na solicitação {e.Message}");
        }

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetVehicleReport(int id)
    {
        var vehicleReport = await _context.VehicleReports.FindAsync(id);

        if (vehicleReport == null) return NotFound("Laudo veicular inexistente.");

        return Ok(vehicleReport);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteVehicleReport(int id)
    {
        if (id <= 0) return BadRequest("Laudo veicular inexistente");

        var vehicle = await _context.VehicleReports.FindAsync(id);
        if (vehicle == null) return BadRequest("Não foi possível remover o laudo veicular.");

        LogDeletion(vehicle, vehicle.NumberReport);

        _context.VehicleReports.Remove(vehicle);

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