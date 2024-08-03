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

    [HttpGet("monthly-mileage/{carId:int}")]
    public async Task<ActionResult<MonthlyMileageDto>> GetMonthlyMileage(int carId)
    {
        if(carId <= 0) return BadRequest("Id inválido.");
        
        var car = await _context
            .Cars
            .Include(c => c.CarMileages)
            .FirstOrDefaultAsync(c => c.Id == carId);

        if (car is null) return BadRequest("Carro não pode ser encontrado.");

        var monthlyMileage = car.CarMileages
            .GroupBy(m => new { m.Date.Year, m.Date.Month })
            .Select(g => new MonthlyMileageDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Mileage = g.Max(c => c.Mileage) - g.Min(c => c.Mileage) 
            })
            .ToList();
        
        return Ok(monthlyMileage);
    }

    [HttpGet("fuel-cost{carId:int}")]
    public async Task<ActionResult<MonthlyFuelCostDto>> GetMonthlyFuelCosts(int carId)
    {
        if (carId <= 0) return BadRequest("Carro não pode ser encontrado.");
        
        var car = await _context
            .Cars
            .Include(c => c.FuelRecords)
            .FirstOrDefaultAsync(c => c.Id == carId);

        if (car is null) return NoContent();

        var monthlyCosts = car.FuelRecords
            .GroupBy(fr => new { fr.Date.Year, fr.Date.Month })
            .Select(g => new MonthlyFuelCostDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalCost = g.Sum(fr => fr.Price * fr.Quantity)
            }).ToList();

        return Ok(monthlyCosts);
    }
}