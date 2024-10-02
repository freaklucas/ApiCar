using ApiCar.Data;
using ApiCar.Models;
using ApiCar.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiCar.Controllers;

[Route("/api/[controller]")]
[ApiController]

public class TestDriveController(ITestDriveService testDriveService) : ControllerBase
{
    private readonly ITestDriveService _testDriveService = testDriveService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TestDrive>>> GetAllTestDrives()
    {
        var testDrives = await _testDriveService.GetAllTestsDrive();
        
        if(!testDrives.Any()) return NoContent();
        
        return Ok(testDrives);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TestDrive>> GetTestDriveById(int id)
    {
        var testDrive = await _testDriveService.GetTestDriveById(id);
        
        if(testDrive == null) return BadRequest("Test drive não encontrado.");
        
        return testDrive;
    }

    [HttpPost]
    public async Task<ActionResult<TestDrive>> CreateTestDrive([FromBody] TestDrive testDrive)
    {
        if (testDrive == null) return BadRequest("Dados inválidos.");

        await _testDriveService.CreateTestDrive(testDrive);

        return CreatedAtAction(nameof(GetTestDriveById), new { id = testDrive.Id }, testDrive);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TestDrive>> UpdateTestDrive([FromBody] TestDrive testDrive, int id)
    {
        if(testDrive == null) return BadRequest("Test drive não encontrado.");
        
        var testDriveUnique = await _testDriveService.GetTestDriveById(id);
        
        if (testDriveUnique == null) return BadRequest("Dados inválidos.");
        

        testDriveUnique.Notes = testDrive.Notes;
        testDriveUnique.ScheduledDate = testDrive.ScheduledDate;

        await _testDriveService.UpdateTestDrive(testDriveUnique, id);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteTestDrive(int id)
    {
        var testDrive = await _testDriveService.GetTestDriveById(id);
        
        if (testDrive == null) return BadRequest("Test drive não encontrado.");

        await _testDriveService.DeleteTestDrive(id);

        return NoContent();
    }
}