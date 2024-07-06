using ApiCar.Data;
using ApiCar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCar.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly Context _context;
    public UserController(Context context)
    {
        _context = context;
    }
    
    [HttpPost]
    public ActionResult<User> Create(User user)
    {
        if (user is null)
        {
            return BadRequest("Pessoa nulla.");
        }
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(user);
    }
}