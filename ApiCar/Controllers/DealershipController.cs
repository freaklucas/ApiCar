using ApiCar.Models;
using ApiCar.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiCar.Controllers;

[Route("/api/[controller]")]
[ApiController]

public class DealershipController(IDealershipService dealershipService) : ControllerBase
{
    private readonly IDealershipService _dealershipService = dealershipService;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dealership>>> GetAllDealerships()
    {
        var dealerships =  await _dealershipService.GetAllDealerships();

        return Ok(dealerships);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Dealership>> GetDealershipById(int id)
    {
        var dealership = await _dealershipService.GeDealershiptById(id);
        
        if(dealership == null) return BadRequest("Concessionária não encontrada.");

        return Ok(dealership);
    }

    [HttpPost]
    public async Task<ActionResult<Dealership>> AddDealership([FromBody] Dealership dealership)
    {
        if(dealership == null) return BadRequest("Dados inválidos.");
        
        await _dealershipService.CreateDealership(dealership);

        return CreatedAtAction(nameof(GetDealershipById), new { id = dealership.Id }, dealership);
    }

    [HttpPut("{int:id}")]
    public async Task<ActionResult<Dealership>> UpdateDealership([FromBody] Dealership dealership, int id)
    {
        if (dealership == null) return BadRequest("Dados inválidos.");
        
        await _dealershipService.UpdateDealership(dealership, id);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteDealership(int id)
    {
        await _dealershipService.DeleteDealership(id);
        
        return NoContent();
    }
}