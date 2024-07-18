using ApiCar.Data;
using ApiCar.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

/// <summary>
///     Relatórios.
/// </summary>
[Route("/api/[controller]")]
[ApiController]
public class ReportsController(Context context) : ControllerBase
{
    private readonly Context _context = context;

    [HttpGet("maintenance-costs/{carId:int}")]
    public async Task<ActionResult<MaintenanceReportDto>> GetMaintenanceCost(int carId)
    {
        var car = await _context
            .Cars
            .Include(c => c.MaintenanceRecords)
            .FirstOrDefaultAsync(c => c.Id == carId);

        if (car is null) return NoContent();

        var totalCost = car.MaintenanceRecords.Sum(m => m.Cost);
        var averageCostPerYear = car
            .MaintenanceRecords
            .GroupBy(c => c.Date.Year)
            .Select(g => new YearlyCostDto { Year = g.Key, AverageCost = g.Average(m => m.Cost) })
            .ToList();

        var report = new MaintenanceReportDto
        {
            CarId = carId,
            TotalCost = totalCost,
            AverageCostPerYear = averageCostPerYear
        };

        return Ok(report);
    }
}