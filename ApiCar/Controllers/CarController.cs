using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CarController : ControllerBase
{
    private readonly Context _context;

    public CarController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetCars()
    {
        return await _context.Cars.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Car>> GetCar(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if(car is null) return NotFound();


        return Ok(car);
    }
    
    [HttpPost]
    public async Task<ActionResult<Car>> PostCar(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
    }

}