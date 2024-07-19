using ApiCar.Data;
using ApiCar.Dtos;
using ApiCar.Models;
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

    [HttpGet("insurance-count")]
    public async Task<ActionResult<int>> GetInsuranceCount()
    {
        var activeInsuranceCount = await _context
            .InsurancePolicy
            .CountAsync(c => c.EndDate > DateTime.Now);

        if (activeInsuranceCount == 0) return BadRequest("Não foi encontrado seguro.");

        return Ok(activeInsuranceCount);
    }

    [HttpGet("maintenance-cost-period")]
    public async Task<ActionResult<MaintenanceRecord>> GetMaintenanceCostPeriod(int carId, DateTime start, DateTime end)
    {
        var car = await _context.Cars.Include(c => c.MaintenanceRecords).FirstOrDefaultAsync();
        if (car is null) return BadRequest("Não existem carros.");

        var maintenancePeriod = car.MaintenanceRecords
            .Where(m => m.Date >= start && m.Date <= end)
            .ToList();

        var totalCost = maintenancePeriod.Sum(c => c.Cost);
        var averageCostPerYear = maintenancePeriod
            .GroupBy(c => c.Date.Year)
            .Select(g => new YearlyCostDto 
            { 
                Year = g.Key, 
                AverageCost = g.Average(m => m.Cost) 
            })
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