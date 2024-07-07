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

    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
        var users = _context.Users.Include(c => c.Cars).ToList();
        if (users is null) return BadRequest("Usuários não encontrados.");

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> GetUser(int id)
    {
        var searchUser = _context.Users.FirstOrDefault(u => u.Id == id);
        
        if(searchUser == null) return BadRequest("Usuário por id não encontrado.");
        
        return Ok(searchUser);
    }

    [HttpGet("/Name/{name}")]
    public ActionResult<User> GetName(string name)
    {
        var users = _context.Users.Include(u => u.Cars).ToList();

        var findName = users.Where(u => u.Name != null && u.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            
        if (findName.Any() == null) return BadRequest("Nome não encontrado.");

        return Ok(findName);
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

    [HttpPut("{id:int}")]
    public ActionResult<User> Update(int id, User user) {
        if (user is null) 
            return NotFound("Usuario nulo.");

        if (id != user.Id) return NotFound("Id nulo.");

        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(user);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Remove(int id)
    {
        var findUser = _context.Users.Find(id);

        if (findUser is null) return BadRequest("Operação não pode ser realizada.");

        _context.Users.Remove(findUser);
        _context.SaveChanges();

        return Ok($"Usuário {findUser.Name} deletado.");
    }
}