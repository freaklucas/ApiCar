using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InsurancePolicyController : ControllerBase
{
    private readonly Context _context;

    public InsurancePolicyController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsurancePolicy>>> GetInsurancePolicies()
    {
        var insurancePolicies = await _context
            .InsurancePolicy
            .AsTracking()
            .ToListAsync();

        if (insurancePolicies == null || !insurancePolicies.Any())
            return NotFound("Nenhum seguro encontrado.");

        return Ok(insurancePolicies);
    }

    [HttpGet("{carId:int}")]
    public async Task<ActionResult<IEnumerable<InsurancePolicy>>> GetInsurancePolicyCar(int carId)
    {
        var insurancePolicies = await _context
            .InsurancePolicy
            .Where(i => i.CarId == carId)
            .AsTracking()
            .Include(i => i.Car)
            .ToListAsync();

        if (insurancePolicies == null || !insurancePolicies.Any())
            return NotFound("Nenhum seguro encontrado.");

        return Ok(insurancePolicies);
    }

    [HttpPost]
    public async Task<ActionResult<InsurancePolicy>> PostInsurancePolicy(InsurancePolicy policy)
    {
        if (policy == null) return BadRequest("Nenhum seguro encontrado.");

        _context.InsurancePolicy.Add(policy);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInsurancePolicyCar), new { carId = policy.CarId }, policy);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutInsurancePolicy(int id, InsurancePolicy policy)
    {
        if (policy == null) return BadRequest("Não foi possível atualizar seguro.");
        if (id != policy.Id) return BadRequest("Ids não correspondem.");

        var findPolicy = await _context.InsurancePolicy.FindAsync(id);
        if (findPolicy == null) return NotFound("Nenhum seguro encontrado.");

        findPolicy.ProviderName = policy.ProviderName;
        findPolicy.Premium = policy.Premium;
        findPolicy.StartDate = policy.StartDate;
        findPolicy.EndDate = policy.EndDate;
        findPolicy.CarId = policy.CarId;
        findPolicy.Deductible = policy.Deductible;
        findPolicy.MaxCoverage = policy.MaxCoverage;
        findPolicy.PeopleCoverage = policy.PeopleCoverage;

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
    public async Task<ActionResult> DeleteInsurancePolicy(int id)
    {
        if (id <= 0) return BadRequest("Apólice de seguro não encontrada.");

        var insurancePolicy = await _context.InsurancePolicy.FindAsync(id);

        if (insurancePolicy == null) return NotFound("Apólice não encontrada.");

        _context.InsurancePolicy.Remove(insurancePolicy);
        await _context.SaveChangesAsync();

        return Accepted(insurancePolicy);
    }
}
