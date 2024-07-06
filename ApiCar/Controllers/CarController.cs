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

    [HttpGet("BuscarPorMarca/{make:string}")]
    public async Task<ActionResult<IEnumerable<Car>>> GetCarByMake(string make)
    {
        return await _context.Cars.Where(c => c.Make == make).ToListAsync();
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

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateCar(int id, Car car)
    {
        if(id != car.Id) return NotFound();
        if(car is null) return NotFound();

        _context.Entry(car).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Car>> DeleteCar(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return NotFound("Carro não encontrado.");

        _context.Cars.Remove(car);
        _context.SaveChangesAsync();

        return Accepted(car);
    }

}