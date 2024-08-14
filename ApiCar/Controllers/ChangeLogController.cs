using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("/api/[controller]")]
[ApiController]

public class ChangeLogController : ControllerBase
{
    private readonly Context _context;

    public ChangeLogController(Context context)
    {
        _context = context;
    }
    
    [HttpPost("test-log")]
    public async Task<IActionResult> TestLog()
    {
        var log = new ChangeLog
        {
            EntityName = "TestEntity",
            EntityId = 1,
            PropertyName = "TestProperty",
            OldValue = "OldValue",
            NewValue = "NewValue",
            ChangeDate = DateTime.Now,
            UserName = "TestUser"
        };

        _context.ChangeLogs.Add(log);
        await _context.SaveChangesAsync();

        return Ok();
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChangeLog>>> GetChangeLogs()
    {
        var logs = await _context.ChangeLogs.AsNoTracking().ToListAsync();
        if (!logs.Any()) return NoContent();
    
        return Ok(logs);
    }
}